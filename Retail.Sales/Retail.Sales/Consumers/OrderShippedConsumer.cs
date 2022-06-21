namespace Retail.Sales.Consumers
{
    using System.Threading.Tasks;
    using Events;
    using MassTransit;
    using Serilog;

    public class OrderShippedConsumer : IConsumer<IOrderShipped>
    {
        public Task Consume(ConsumeContext<IOrderShipped> context)
        {
            Log.Information($"Order {context.Message.OrderId} processing finished.");
            return Task.CompletedTask;
        }
    }
}


