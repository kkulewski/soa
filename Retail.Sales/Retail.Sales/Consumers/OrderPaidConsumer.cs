namespace Retail.Sales.Consumers
{
    using System.Threading.Tasks;
    using Events;
    using MassTransit;
    using Serilog;

    public class OrderPaidConsumer : IConsumer<IOrderPaid>
    {
        public async Task Consume(ConsumeContext<IOrderPaid> context)
        {
            Log.Information($"Order {context.Message.OrderId} confirmed.");
            await context.Publish<IOrderConfirmed>(new { context.Message.OrderId });
        }
    }
}


