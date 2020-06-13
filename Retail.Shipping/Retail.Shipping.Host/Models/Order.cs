namespace Retail.Shipping.Host.Models
{
    using System.Collections.Generic;

    public class Order
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public List<Product> Products { get; set; }
        public OrderState State { get; set; }
    }
}
