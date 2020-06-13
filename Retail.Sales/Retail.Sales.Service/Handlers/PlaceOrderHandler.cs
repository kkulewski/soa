namespace Retail.Sales.Service.Handlers
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
            var orderPlacedEvent = new OrderPlaced
            {
                OrderId = message.OrderId,
                CustomerId = message.CustomerId,
                Products = message.Products
            };

            await context.Publish(orderPlacedEvent);
            this.log.Info($"Order {message.OrderId} from customer {message.CustomerId} received.");
        }
    }
}


