using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace webApp.Models
{
    public class Order
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("order_id")]
        public string order_id { get; set; }

        [BsonElement("customer_id")]
        public string customer_id { get; set; }

        [BsonElement("payment_id")]
        public string payment_id { get; set; }

        [BsonElement("delivery_id")]
        public string delivery_id { get; set; }


        [BsonElement("products")]
        public List<Product> products { get; set; }

        [BsonElement("total_amount")]
        public decimal total_amount { get; set; }

        [BsonElement("status")]
        public string status { get; set; }

        [BsonElement("payment_status")]
        public string payment_status { get; set; }

        [BsonElement("product_status")]
        public string product_status { get; set; }

        [BsonElement("delivery_address")]
        public string delivery_address { get; set; }

        [BsonElement("delivery_status")]
        public string delivery_status { get; set; }

        [BsonElement("timestamp")]
        public DateTime timestamp { get; set; }

        [BsonElement("__v")]
        public int __v { get; set; }

        public Customer customer { get; set; }
        public Payment payments { get; set; }
    }

    public class Product
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("quantity")]
        public int quantity { get; set; }

        [BsonElement("price")]
        public decimal price { get; set; }
    }
}
