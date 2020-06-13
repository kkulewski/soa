namespace Retail.Billing.Host.Handlers
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
            this.log.Info($"Collected payment for order {message.OrderId} from customer {message.CustomerId}");
            var orderPaid = new OrderPaid { OrderId = message.OrderId };
            await context.Publish(orderPaid);
        }
    }
}


