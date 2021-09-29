namespace Retail.Commands
{
    using System.Collections.Generic;
    using Sales.Service.Models;

    public interface IPlaceOrder
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
