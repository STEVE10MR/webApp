using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace webApp.Models
{
    public class Payment
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("payment_id")]
        public string payment_id { get; set; }

        [BsonElement("payment_method")]
        public string payment_method { get; set; }

        [BsonElement("status")]
        public string status { get; set; }

        [BsonElement("timestamp")]
        public DateTime timestamp { get; set; }

        [BsonElement("__v")]
        public int __v { get; set; }
    }
}