namespace Retail.Shipping.Host.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Collections.Generic;

    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public List<Product> Products { get; set; }
        public OrderState State { get; set; }
    }
}
