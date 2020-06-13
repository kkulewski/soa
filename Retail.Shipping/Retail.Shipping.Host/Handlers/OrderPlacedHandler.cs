namespace Retail.Shipping.Host.Handlers
{
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Logging;
    using Events;
    using Models;
    using Repositories;

    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        private readonly ILog log;
        private readonly IOrderRepository orders;

        public OrderPlacedHandler(ILog log, IOrderRepository orders)
        {
            this.log = log;
            this.orders = orders;
        }

        public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            var newOrder = new Order
            {
                OrderId = message.OrderId,
                CustomerId = message.CustomerId,
                Products = message.Products,
                State = OrderState.Received
            };

            await orders.Add(newOrder);
            this.log.Info($"Order {message.OrderId} received. Waiting for confirmation.");
        }
    }
}


