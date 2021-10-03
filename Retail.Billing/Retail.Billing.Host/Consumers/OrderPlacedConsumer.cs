namespace Retail.Billing.Host.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using Events;
    using System;

    public class OrderPlacedConsumer : IConsumer<IOrderPlaced>
    {
        public async Task Consume(ConsumeContext<IOrderPlaced> context)
        {
            Console.WriteLine($"Collected payment for order {context.Message.OrderId} from customer {context.Message.CustomerId}");

            await context.Publish<IOrderPaid>(new
            {
                context.Message.OrderId
            });
        }
    }
}


