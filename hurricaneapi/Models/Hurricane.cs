using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace hurricaneapi.Models
{
    public class Hurricane
    {
        [BsonId]
        public string id { get; set; }
        [BsonElement("name")]
        [JsonPropertyName("name")]
        public string name { get; set; }
        [BsonElement("coordinates")]
        [JsonPropertyName("coordinates")]
        public IEnumerable<double[]> coordsList { get; set; }
        [BsonElement("times")]
        [JsonPropertyName("times")]
        public IEnumerable<long> timeList { get; set; }
        [BsonElement("speed")]
        [JsonPropertyName("speed")]
        public IEnumerable<int> speedList { get; set; }
        [BsonElement("active")]
        [JsonPropertyName("active")]
        public bool IsActive { get; set; }
        [BsonElement("maxSpeed")]
        [JsonPropertyName("maxSpeed")]
        public int maxSpeed {get; set;}
        public Hurricane(string id,IEnumerable<double[]> coordsList,IEnumerable<long> timeList, IEnumerable<int> speedList, string name, bool IsActive, int maxSpeed)
        {
            this.id = id;
            this.coordsList = coordsList;
            this.name = name;
            this.timeList = timeList;
            this.IsActive = IsActive;
            this.speedList = speedList;
            this.maxSpeed = maxSpeed;
        }
        
        
    }
}