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

        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            this.log.Info($"Order {message.OrderId} with {message.Products.Count} items from customer {message.CustomerId} received!");
            return Task.CompletedTask;
        }
    }
}


