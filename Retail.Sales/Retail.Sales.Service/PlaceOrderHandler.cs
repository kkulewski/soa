namespace Retail.Sales.Service
{
    using System.Threading.Tasks;
    using Frontend.Messages;
    using NServiceBus;
    using NServiceBus.Logging;

    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        private readonly ILog log;

        public PlaceOrderHandler(ILog log)
        {
            this.log = log;
        }

        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            this.log.Info($"Order {message.OrderId} received!");
            return Task.CompletedTask;
        }
    }
}


