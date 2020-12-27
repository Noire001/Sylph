using System.Collections.Generic;
using System.IO;
using System.Linq;
using hurricaneapi.Models;
using MongoDB.Driver;

namespace hurricaneapi.Services
{
    public class HurricaneService
    {
        private IMongoCollection<Hurricane> collection;
        public HurricaneService(HurricaneDatabaseSettings.IHurricaneDatabaseSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);
            collection = database.GetCollection<Hurricane>(settings.HurricaneCollectionName);
        }

        public List<Hurricane> GetAllHurricanes()
        {
            return collection.Find(hurricane => true).ToList();
        }

        public List<Hurricane> GetHurricane(long startdate, long enddate, int maxspeed)
        {
            
            var filter = Builders<Hurricane>.Filter;
            var startDateFilter = filter.Gte("times.0", startdate);
            var endDateFilter = filter.Lte("times.0", enddate);
            var maxSpeedFilter = filter.Lte("speed.0", maxspeed);
            return collection.Find(startDateFilter & endDateFilter & maxSpeedFilter).ToList();
        }
    }
}