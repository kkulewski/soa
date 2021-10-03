namespace Retail.Events
{
    using System.Collections.Generic;
    using Sales.Service.Models;

    public interface IOrderPlaced
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
