using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Sylph.Models
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
        [BsonElement("stormSpeed")]
        [JsonPropertyName("stormSpeed")]
        public int stormSpeed { get; set; }
        [BsonElement("windSpeed")]
        [JsonPropertyName("windSpeed")]
        public int windSpeed { get; set; }
        [BsonElement("cat")] 
        [JsonPropertyName("cat")]
        public int category { get; set; }
        [BsonElement("dist2land")] 
        [JsonPropertyName("dist2land")]
        public int distanceToLand { get; set; }
        
        public DataPoints(double lat, double lon, long time, int stormSpeed, int windSpeed, int category, int distanceToLand)
        {
            this.lat = lat;
            this.lon = lon;
            this.time = time;
            this.stormSpeed = stormSpeed;
            this.windSpeed = windSpeed;
            this.category= category;
            this.distanceToLand = distanceToLand;
        }
    }
    public class Hurricane
    {
        [BsonId]
        public string id { get; set; }
        [BsonElement("name")]
        [JsonPropertyName("name")]
        public string name { get; set; }
        [BsonElement("active")]
        [JsonPropertyName("active")]
        public bool IsActive { get; set; }
        [BsonElement("maxSpeed")]
        [JsonPropertyName("maxSpeed")]
        public int maxSpeed {get; set;}
        [BsonElement("firstActive")]
        [JsonPropertyName("firstActive")]
        public long firstActive { get; set; }
        [BsonElement("lastActive")]
        [JsonPropertyName("lastActive")]
        public long lastActive { get; set; }
        [BsonElement("datapoints")]
        [JsonPropertyName("datapoints")]
        public IEnumerable<DataPoints> dataPointsEnumerable { get; set; }
        public Hurricane(string id,IEnumerable<DataPoints> dataPointsEnumerable, string name, bool IsActive, 
            int maxSpeed, long firstActive, long lastActive)
        {
            this.id = id;
            this.dataPointsEnumerable = dataPointsEnumerable;
            this.name = name;
            this.IsActive = IsActive;
            this.maxSpeed = maxSpeed;
            this.firstActive = firstActive;
            this.lastActive = lastActive;
        }
        
        
    }
}