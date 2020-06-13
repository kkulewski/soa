namespace Retail.Recommendations.Service.Events
{
    using System.Collections.Generic;
    using Models;

    public class OrderPlaced
    {
        public string OrderId { get; set; }
        public List<Product> Products { get; set; }
    }
}
