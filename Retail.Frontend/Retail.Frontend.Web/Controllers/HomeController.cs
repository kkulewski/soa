using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Retail.Frontend.Messages;
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
            var command = new PlaceOrder { OrderId = orderId };

            await bus.Send(command).ConfigureAwait(false);

            var vm = new OrderPlaced { OrderId = orderId };
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
