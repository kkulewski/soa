namespace Retail.Events
{
    using System.Collections.Generic;
    using Retail.Shipping.Host.Models;

    public interface IOrderPlaced
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
