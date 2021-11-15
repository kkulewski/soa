using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Retail.Commands;
using Retail.Sales.Dto;
using Retail.Sales.Models;

namespace Retail.Sales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> logger;

        private readonly ISendEndpointProvider sendEndpointProvider;

        public OrdersController(ILogger<OrdersController> logger, ISendEndpointProvider sendEndpointProvider)
        {
            this.logger = logger;
            this.sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderDto order)
        {
            var orderId = Guid.NewGuid().ToString();
            var customerId = ((uint)this.HttpContext.Connection.RemoteIpAddress.GetHashCode()).ToString();
            var products = new List<Product> { new Product { ProductId = order.ProductId } };

            var endpoint = await this.sendEndpointProvider.GetSendEndpoint(new Uri("queue:sales"));
            await endpoint.Send<IPlaceOrder>(new { orderId, customerId, products });

            this.logger.LogInformation($"Order {orderId} submitted");

            return Ok();
        }
    }
}
