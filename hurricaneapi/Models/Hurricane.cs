using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace hurricaneapi.Models
{
    public class Hurricane
    {
        [BsonId]
        public string id { get; set; }
        [BsonElement("coordinates")]
        public IEnumerable<double[]> coordsList { get; set; }

        public Hurricane(string id,IEnumerable<double[]> coordsList)
        {
            this.id = id;
            this.coordsList = coordsList;
        }
        
        
    }
}