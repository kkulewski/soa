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
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> logger;

        private readonly ISendEndpointProvider sendEndpointProvider;

        private readonly Dictionary<string, decimal> priceTable;

        public OrderController(ILogger<OrderController> logger, ISendEndpointProvider sendEndpointProvider)
        {
            this.logger = logger;
            this.sendEndpointProvider = sendEndpointProvider;
            this.priceTable = new Dictionary<string, decimal>
            {
                ["100"] = 39.99M,
                ["101"] = 50.00M,
                ["102"] = 75.00M
            };
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(
                "<form action=\"http://localhost:5002/order\" method=\"post\">" +
                $"<input type=\"hidden\" name=\"productId\" value=\"{id}\">" +
                $"<input type=\"submit\" class=\"btn btn-outline-success\" value=\"Buy now for ${this.priceTable[id]}\" />" +
                "</form>");
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromForm]OrderDto order)
        {
            var orderId = Guid.NewGuid().ToString();
            var customerId = ((uint)this.HttpContext.Connection.RemoteIpAddress.GetHashCode()).ToString();
            var products = new List<Product> { new Product { ProductId = order.ProductId } };

            var endpoint = await this.sendEndpointProvider.GetSendEndpoint(new Uri("queue:sales"));
            await endpoint.Send<IPlaceOrder>(new { orderId, customerId, products });

            this.logger.LogInformation($"Order {orderId} submitted");

            return Redirect($"http://localhost:5000/home/orderplaced/{orderId}");
        }
    }
}
