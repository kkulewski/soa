namespace Retail.Sales.Service.Consumers
{
    using System;
    using System.Threading.Tasks;
    using Events;
    using MassTransit;

    public class OrderPaidConsumer : IConsumer<IOrderPaid>
    {
        public async Task Consume(ConsumeContext<IOrderPaid> context)
        {
            Console.WriteLine($"Order {context.Message.OrderId} confirmed.");
            await context.Publish<IOrderConfirmed>(new { context.Message.OrderId });
        }
    }
}


