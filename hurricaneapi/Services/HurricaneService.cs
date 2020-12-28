using System.Collections.Generic;
using System.IO;
using System.Linq;
using hurricaneapi.Models;
using MongoDB.Bson;
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

        public List<Hurricane> GetHurricane(long startdate, long enddate, int maxspeed, short active, string name, string sortorder)
        {
            
            var filter = Builders<Hurricane>.Filter;
            
            var startDateFilter = filter.Gte("times.0", startdate);
            var endDateFilter = filter.Lte("times.0", enddate);
            var maxSpeedFilter = filter.Lte("maxSpeed", maxspeed);
            var activeFilter = filter.Eq("active", active == 1?true:false);
            var nameFilter = filter.Regex("name", new BsonRegularExpression(name.ToUpper()));
            var sortDefinition = Builders<Hurricane>.Sort.Descending(hurricane => hurricane.id);
            if (sortorder.Equals("asc"))
            {
                sortDefinition = Builders<Hurricane>.Sort.Ascending(hurricane => hurricane.id);
            }

            if (active != 0 && active != 1) //not filtering activity, show both active & inactive
                return collection.Find(startDateFilter & endDateFilter & maxSpeedFilter & nameFilter).Sort(sortDefinition).ToList();
            //we are filtering activity, return either active or non-active explicitly
            return collection.Find(startDateFilter & endDateFilter & maxSpeedFilter & activeFilter & nameFilter).Sort(sortDefinition).ToList();
        }
    }
}