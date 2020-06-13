namespace Retail.Sales.Service.Handlers
{
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Logging;
    using Events;

    public class OrderPaidHandler : IHandleMessages<OrderPaid>
    {
        private readonly ILog log;

        public OrderPaidHandler(ILog log)
        {
            this.log = log;
        }

        public async Task Handle(OrderPaid message, IMessageHandlerContext context)
        {
            this.log.Info($"Order {message.OrderId} confirmed.");

            var confirmedOrder = new OrderConfirmed
            {
                OrderId = message.OrderId
            };
            await context.Publish(confirmedOrder);
        }
    }
}


