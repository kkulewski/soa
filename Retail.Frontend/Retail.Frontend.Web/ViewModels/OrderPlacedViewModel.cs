using System.Collections.Generic;
using Retail.Frontend.Web.Models;

namespace Retail.Frontend.Web.ViewModels
{
    public class OrderPlacedViewModel
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
