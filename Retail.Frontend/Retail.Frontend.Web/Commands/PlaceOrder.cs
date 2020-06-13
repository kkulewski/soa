namespace Retail.Frontend.Web.Commands
{
    using System.Collections.Generic;
    using Models;

    public class PlaceOrder
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
