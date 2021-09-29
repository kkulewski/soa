namespace Retail.Sales.Service.Consumers
{
    using System;
    using System.Threading.Tasks;
    using Commands;
    using Events;
    using MassTransit;

    public class PlaceOrderConsumer : IConsumer<IPlaceOrder>
    {
        public async Task Consume(ConsumeContext<IPlaceOrder>  context)
        {
            Console.WriteLine($"Order {context.Message.OrderId} from customer {context.Message.CustomerId} received.");
            await context.Publish<IOrderPlaced>(new { context.Message.OrderId, context.Message.CustomerId, context.Message.Products });
        }
    }
}


