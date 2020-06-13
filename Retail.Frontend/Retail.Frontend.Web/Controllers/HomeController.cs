using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Retail.Frontend.Web.Messages;
using Retail.Frontend.Web.Models;
using Retail.Frontend.Web.ViewModels;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Retail.Frontend.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IMessageSession bus;

        public HomeController(ILogger<HomeController> logger, IMessageSession bus)
        {
            this.logger = logger;
            this.bus = bus;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://retail-catalog/product");
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var vm = JsonSerializer.Deserialize<List<ProductViewModel>>(json, options);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(ProductViewModel productVm)
        {
            var products = new List<Product>
            {
                new Product { ProductId = productVm.ProductId }
            };

            var command = new PlaceOrder
            {
                OrderId = Guid.NewGuid().ToString(),
                CustomerId = ((uint)this.HttpContext.Connection.RemoteIpAddress.GetHashCode()).ToString(),
                Products = products
            };

            await bus.Send(command).ConfigureAwait(false);

            var vm = new OrderViewModel
            {
                OrderId = command.OrderId,
                CustomerId = command.CustomerId,
                Products = command.Products
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
