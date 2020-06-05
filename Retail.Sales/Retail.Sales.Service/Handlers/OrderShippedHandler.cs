namespace Retail.Sales.Service.Handlers
{
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Logging;
    using Messages;

    public class OrderShippedHandler : IHandleMessages<OrderShipped>
    {
        private readonly ILog log;

        public OrderShippedHandler(ILog log)
        {
            this.log = log;
        }

        public Task Handle(OrderShipped message, IMessageHandlerContext context)
        {
            this.log.Info($"Order {message.OrderId} processing finished.");
            return Task.CompletedTask;
        }
    }
}


