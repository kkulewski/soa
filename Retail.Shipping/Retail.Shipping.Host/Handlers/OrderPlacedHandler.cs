namespace Retail.Shipping.Host.Handlers
{
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Logging;
    using Messages;

    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        private readonly ILog log;

        public OrderPlacedHandler(ILog log)
        {
            this.log = log;
        }

        public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            var shipping = new OrderShipped { OrderId = message.OrderId };
            await context.Publish(shipping);
            this.log.Info($"Shipped order {message.OrderId} to customer {message.CustomerId}");
        }
    }
}


