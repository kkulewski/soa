using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Retail.Frontend.Web.Messages;
using Retail.Frontend.Web.Models;
using Retail.Frontend.Web.ViewModels;

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

        public IActionResult Index()
        {
            var products = new List<Product>();
            var vm = new OrderViewModel
            {
                OrderId = string.Empty,
                CustomerId = ((uint)this.HttpContext.Connection.RemoteIpAddress.GetHashCode()).ToString(),
                Products = products,
                ProductIds = string.Empty
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderViewModel vm)
        {
            vm.OrderId = Guid.NewGuid().ToString();
            vm.Products = vm
                .ProductIds
                .Split(new [] {';', ',', ' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(id => new Product { ProductId = id })
                .ToList();

            var command = new PlaceOrder
            {
                OrderId = vm.OrderId,
                CustomerId = vm.CustomerId,
                Products = vm.Products
            };

            await bus.Send(command).ConfigureAwait(false);

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
