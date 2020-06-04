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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var orderId = Guid.NewGuid().ToString();
            var customerId = Guid.NewGuid().ToString();
            var products = new List<Product>();
            var command = new PlaceOrder { OrderId = orderId, CustomerId = customerId, Products = products };

            await bus.Send(command).ConfigureAwait(false);

            var vm = new OrderPlacedViewModel { OrderId = orderId, CustomerId = customerId, Products = products };
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
