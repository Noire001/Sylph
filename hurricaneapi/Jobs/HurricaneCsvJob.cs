using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using hurricaneapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using Quartz;

namespace hurricaneapi.Jobs
{
    public class HurricaneCsvJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            List<string[]> rowList = new List<string[]>();
            List<Hurricane> hurricaneList = new List<Hurricane>();
            using (WebClient client = new WebClient())
            {
                client.DownloadFile("https://www.ncei.noaa.gov/data/international-best-track-archive-for-climate-stewardship-ibtracs/v04r00/access/csv/ibtracs.ACTIVE.list.v04r00.csv","Data/active.csv");
                client.DownloadFile("https://www.ncei.noaa.gov/data/international-best-track-archive-for-climate-stewardship-ibtracs/v04r00/access/csv/ibtracs.last3years.list.v04r00.csv","Data/last3years.csv");
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

            for (int i = 2; i < rowList.Count-1; i++)
            {
                if (rowList[i][0] != rowList[i+1][0] || (i+3) == rowList.Count)
                    hurricaneList.Add(new Hurricane(rowList[i][0]));
            }
            File.WriteAllText("hurricanes.json",JsonSerializer.Serialize(hurricaneList));
            return Task.CompletedTask;
        }
    }
}