namespace Retail.Shipping.Host.Consumers
{
    using System;
    using System.Threading.Tasks;
    using Events;
    using Repositories;
    using Models;
    using MassTransit;

    public class OrderConfirmedConsumer : IConsumer<IOrderConfirmed>
    {
        public async Task Consume(ConsumeContext<IOrderConfirmed> context)
        {
            var orders = new MongoOrderRepository();

            var orderToShip = await orders.GetById(context.Message.OrderId);

            if (orderToShip == null)
            {
                throw new Exception($"Order {context.Message.OrderId} was not received yet.");
            }

            orderToShip.State = OrderState.Shipped;
            await orders.Update(orderToShip);
            Console.WriteLine($"Shipped {orderToShip.Products.Count} items to customer {orderToShip.CustomerId} (order {orderToShip.OrderId})");

            await context.Publish<IOrderShipped>(new { context.Message.OrderId });
        }
    }
}


