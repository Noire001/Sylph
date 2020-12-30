using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace hurricaneapi.Models
{
    public class DataPoints
    {
        [BsonElement("lat")]
        [JsonPropertyName("lat")]
        public double lat { get; set; }
        [BsonElement("lon")]
        [JsonPropertyName("lon")]
        public double lon { get; set; }
        [BsonElement("time")]
        [JsonPropertyName("time")]
        public long time { get; set; }
        [BsonElement("speed")]
        [JsonPropertyName("speed")]
        public int speed { get; set; }
        
        public DataPoints(double lat, double lon, long time, int speed)
        {
            
            this.lat = lat;
            this.lon = lon;
            this.time = time;
            this.speed = speed;
        }
    }
    public class Hurricane
    {
        [BsonId]
        public string id { get; set; }
        [BsonElement("name")]
        [JsonPropertyName("name")]
        public string name { get; set; }
        
        [BsonElement("datapoints")]
        [JsonPropertyName("datapoints")]
        public IEnumerable<DataPoints> coordsList { get; set; }
        [BsonElement("active")]
        [JsonPropertyName("active")]
        public bool IsActive { get; set; }
        [BsonElement("maxSpeed")]
        [JsonPropertyName("maxSpeed")]
        public int maxSpeed {get; set;}
        public Hurricane(string id,IEnumerable<DataPoints> coordsList, string name, bool IsActive, int maxSpeed)
        {
            this.id = id;
            this.coordsList = coordsList;
            this.name = name;
            this.IsActive = IsActive;
            this.maxSpeed = maxSpeed;
        }
        
        
    }
}