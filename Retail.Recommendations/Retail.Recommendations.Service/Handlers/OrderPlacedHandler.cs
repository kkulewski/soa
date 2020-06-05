using System;
using System.Linq;

namespace Retail.Recommendations.Service.Handlers
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

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            this.log.Info($"Order {message.OrderId} products: [{string.Join(", ", message.Products.Select(p => p.ProductId))}]");
            return Task.CompletedTask;
        }
    }
}


