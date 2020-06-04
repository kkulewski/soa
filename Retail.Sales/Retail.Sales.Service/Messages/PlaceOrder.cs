namespace Retail.Sales.Service.Messages
{
    using NServiceBus;
    using System.Collections.Generic;
    using Models;

    public class PlaceOrder : ICommand
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
