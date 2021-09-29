using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Retail.Commands;
using Retail.Frontend.Web.Models;
using Retail.Frontend.Web.ViewModels;

namespace Retail.Frontend.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ISendEndpointProvider sendEndpointProvider;

        public HomeController(ILogger<HomeController> logger, ISendEndpointProvider sendEndpointProvider)
        {
            this.logger = logger;
            this.sendEndpointProvider = sendEndpointProvider;
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
            var orderId = Guid.NewGuid().ToString();
            var customerId = ((uint)this.HttpContext.Connection.RemoteIpAddress.GetHashCode()).ToString();
            var products = new List<Product> { new Product { ProductId = productVm.ProductId } };

            var endpoint = await this.sendEndpointProvider.GetSendEndpoint(new Uri("queue:sales"));
            await endpoint.Send<IPlaceOrder>(new { orderId, customerId, products });

            this.logger.LogInformation($"Order {orderId} submitted");

            return View(new OrderViewModel { OrderId = orderId, CustomerId = customerId, Products = products });
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
