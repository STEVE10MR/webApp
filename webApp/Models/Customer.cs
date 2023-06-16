using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace webApp.Models
{
    public class Customer
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("phone_number")]
        public string phone_number { get; set; }

        [BsonElement("customer_id")]
        public string customer_id { get; set; }

        [BsonElement("status")]
        public string status { get; set; }
      
        [BsonElement("payment_history")]
        public List<object> payment_history { get; set; }

        [BsonElement("return_history")]
        public List<object> return_history { get; set; }

        [BsonElement("delivery_history")]
        public List<object> delivery_history { get; set; }

        [BsonElement("conversation_history")]
        public List<object> conversation_history { get; set; }

        [BsonElement("order_history")]
        public List<object> order_history { get; set; }

        [BsonElement("__v")]
        public int __v { get; set; }
    }

}