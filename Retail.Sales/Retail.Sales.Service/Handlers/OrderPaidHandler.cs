namespace Retail.Sales.Service.Handlers
{
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Logging;
    using Messages;

    public class OrderPaidHandler : IHandleMessages<OrderPaid>
    {
        private readonly ILog log;

        public OrderPaidHandler(ILog log)
        {
            this.log = log;
        }

        public async Task Handle(OrderPaid message, IMessageHandlerContext context)
        {
            var confirmedOrder = new OrderConfirmed
            {
                OrderId = message.OrderId
            };

            await context.Publish(confirmedOrder);

            this.log.Info($"Order {message.OrderId} confirmed.");
        }
    }
}


