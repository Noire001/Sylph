using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace hurricaneapi.Models
{
    public class Hurricane
    {
        [BsonId]
        public string id { get; set; }
        public Hurricane(string id)
        {
            this.id = id;
        }
    }
}