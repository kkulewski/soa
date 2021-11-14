using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Retail.Sales.Models;

namespace Retail.Sales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly Dictionary<string, decimal> priceTable = new Dictionary<string, decimal>
        {
            ["100"] = 39.99M,
            ["101"] = 50.00M,
            ["102"] = 75.00M
        };

        private readonly Dictionary<string, bool> availability = new Dictionary<string, bool>
        {
            ["100"] = true,
            ["101"] = false,
            ["102"] = true
        };

        public ProductsController()
        {
        }

        [HttpGet("{id}")]
        public Product Get(string id)
        {
            return new Product { ProductId = id, Price = this.priceTable[id], IsAvailable = this.availability[id] };
        }
    }
}
