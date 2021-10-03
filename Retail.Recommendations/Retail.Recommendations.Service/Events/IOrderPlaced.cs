namespace Retail.Events
{
    using System.Collections.Generic;
    using Recommendations.Service.Models;

    public interface IOrderPlaced
    {
        public string OrderId { get; set; }
        public List<Product> Products { get; set; }
    }
}
