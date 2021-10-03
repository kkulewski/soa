namespace Retail.Recommendations.Service.Consumers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Events;
    using MassTransit;

    public class OrderPlacedConsumer : IConsumer<IOrderPlaced>
    {
        public Task Consume(ConsumeContext<IOrderPlaced> context)
        {
            Console.WriteLine($"Order {context.Message.OrderId} products: [{string.Join(", ", context.Message.Products.Select(p => p.ProductId))}]");
            return Task.CompletedTask;
        }
    }
}


