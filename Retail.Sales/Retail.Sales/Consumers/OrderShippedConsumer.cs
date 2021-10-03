namespace Retail.Sales.Consumers
{
    using System;
    using System.Threading.Tasks;
    using Events;
    using MassTransit;

    public class OrderShippedConsumer : IConsumer<IOrderShipped>
    {
        public Task Consume(ConsumeContext<IOrderShipped> context)
        {
            Console.WriteLine($"Order {context.Message.OrderId} processing finished.");
            return Task.CompletedTask;
        }
    }
}


