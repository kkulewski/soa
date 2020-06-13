namespace Retail.Shipping.Host.Events
{
    using System.Collections.Generic;
    using Models;

    public class OrderPlaced
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
