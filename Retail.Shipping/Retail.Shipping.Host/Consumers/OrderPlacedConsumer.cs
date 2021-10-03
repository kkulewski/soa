namespace Retail.Shipping.Host.Consumers
{
    using System.Threading.Tasks;
    using Events;
    using Models;
    using Repositories;
    using MassTransit;
    using System;

    public class OrderPlacedConsumer : IConsumer<IOrderPlaced>
    {
        public async Task Consume(ConsumeContext<IOrderPlaced> context)
        {
            var newOrder = new Order
            {
                OrderId = context.Message.OrderId,
                CustomerId = context.Message.CustomerId,
                Products = context.Message.Products,
                State = OrderState.Received
            };

            await new OrderRepository().Add(newOrder);
            Console.WriteLine($"Order {context.Message.OrderId} received. Waiting for confirmation.");
        }
    }
}


