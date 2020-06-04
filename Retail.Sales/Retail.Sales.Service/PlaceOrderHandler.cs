namespace Retail.Sales.Service
{
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Logging;
    using Messages;

    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        private readonly ILog log;

        public PlaceOrderHandler(ILog log)
        {
            this.log = log;
        }

        public async Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            this.log.Info($"Order {message.OrderId} with {message.Products.Count} items from customer {message.CustomerId} received!");

            var orderPlacedEvent = new OrderPlaced
            {
                OrderId = message.OrderId,
                CustomerId = message.CustomerId,
                Products = message.Products
            };

            this.log.Info($"Order {message.OrderId} published!");

            await context.Publish(orderPlacedEvent);
        }
    }
}


