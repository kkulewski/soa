﻿namespace Retail.Sales.Service.Handlers
{
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Logging;
    using Commands;
    using Events;

    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        private readonly ILog log;

        public PlaceOrderHandler(ILog log)
        {
            this.log = log;
        }

        public async Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            this.log.Info($"Order {message.OrderId} from customer {message.CustomerId} received.");

            var orderPlacedEvent = new OrderPlaced
            {
                OrderId = message.OrderId,
                CustomerId = message.CustomerId,
                Products = message.Products
            };
            await context.Publish(orderPlacedEvent);
        }
    }
}


