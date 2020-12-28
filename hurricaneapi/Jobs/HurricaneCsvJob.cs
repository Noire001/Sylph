using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using hurricaneapi.Models;
using Microsoft.VisualBasic.FileIO;
using MongoDB.Bson;
using MongoDB.Driver;
using Quartz;

namespace hurricaneapi.Jobs
{
    public class HurricaneCsvJob : IJob
    {
        private IMongoCollection<Hurricane> collection;

        public HurricaneCsvJob(HurricaneDatabaseSettings.IHurricaneDatabaseSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);
            collection = database.GetCollection<Hurricane>(settings.HurricaneCollectionName);
        }

        public Task Execute(IJobExecutionContext context)
        {
            List<string[]> rowList = new List<string[]>();

            List<Hurricane> hurricaneList = new List<Hurricane>();
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(
                    "https://www.ncei.noaa.gov/data/international-best-track-archive-for-climate-stewardship-ibtracs/v04r00/access/csv/ibtracs.ACTIVE.list.v04r00.csv",
                    "Data/active.csv");
            }

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(
                    "https://www.ncei.noaa.gov/data/international-best-track-archive-for-climate-stewardship-ibtracs/v04r00/access/csv/ibtracs.last3years.list.v04r00.csv",
                    "Data/last3years.csv");
            }

            var parser = new TextFieldParser("Data/last3years.csv");
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            rowList.Clear();
            while (!parser.EndOfData)
            {
                string[] row = parser.ReadFields();
                rowList.Add(row);
            }

            List<double[]> coordsList = new List<double[]>();
            List<long> timeList = new List<long>();
            List<int> speedList = new List<int>();
            int maxSpeed = 0;
            for (int i = 2; i < rowList.Count - 1; i++)
            {
                coordsList.Add(new[] {Convert.ToDouble(rowList[i][8]), Convert.ToDouble(rowList[i][9])});
                DateTime dateTime = DateTime.ParseExact(rowList[i][6], "yyyy-MM-dd HH:mm:ss", null);
                long unixTime = ((DateTimeOffset) dateTime).ToUnixTimeMilliseconds();
                timeList.Add(unixTime);
                speedList.Add(Convert.ToInt32(rowList[i][161]));
                if (Convert.ToInt32(rowList[i][161]) > maxSpeed){
                    maxSpeed = Convert.ToInt32(rowList[i][161]);
                }
                    
                if (rowList[i][0] != rowList[i + 1][0] || (i + 3) == rowList.Count)
                {
                    hurricaneList.Add(new Hurricane(rowList[i][0], new List<double[]>(coordsList),
                        new List<long>(timeList), new List<int>(speedList), rowList[i][5], false, maxSpeed));
                    coordsList.Clear();
                    timeList.Clear();
                    speedList.Clear();
                    maxSpeed = 0;
                }
            }

            var activeParser = new TextFieldParser("Data/active.csv");
            activeParser.TextFieldType = FieldType.Delimited;
            activeParser.SetDelimiters(",");
            rowList.Clear();
            while (!activeParser.EndOfData)
            {
                string[] row = activeParser.ReadFields();
                rowList.Add(row);
            }

            int numOfActiveHurricanes = 0;
            List<string> activeHurricaneIds = new List<string>();
            for (int i = 2; i < rowList.Count - 1; i++)
            {
                if (rowList[i][0] != rowList[i + 1][0] || (i + 3) == rowList.Count)
                {
                    activeHurricaneIds.Add(rowList[i][0]);
                    numOfActiveHurricanes++;
                }
            }

            for (int i = 0; i < numOfActiveHurricanes; i++)
            {
                for (int j = hurricaneList.Count - 1; j >= 0; j--)
                {
                    if (activeHurricaneIds[i] != hurricaneList[j].id) continue;
                    hurricaneList[j].IsActive = true;
                    break;
                }
            }

            InsertManyOptions options = new InsertManyOptions();
            options.IsOrdered = false;
            try
            {
                collection.DeleteMany(hurricane => true);
                collection.InsertMany(hurricaneList, options);
            }
            catch (MongoBulkWriteException e)
            {
            }

            File.WriteAllText("hurricanes.json",
                JsonSerializer.Serialize(collection.Find(hurricane => true).SortByDescending(hurricane => hurricane.id)
                    .ToList()));
            return Task.CompletedTask;
        }
    }
}