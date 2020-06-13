

namespace Retail.Shipping.Host.Handlers
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Logging;
    using Messages;
    using Repositories;
    using Models;

    public class OrderConfirmedHandler : IHandleMessages<OrderConfirmed>
    {
        private readonly ILog log;
        private readonly IOrderRepository orders;

        public OrderConfirmedHandler(ILog log, IOrderRepository orders)
        {
            this.log = log;
            this.orders = orders;
        }

        public async Task Handle(OrderConfirmed message, IMessageHandlerContext context)
        {
            var orderToShip = await orders.GetById(message.OrderId);

            if (orderToShip == null)
            {
                throw new Exception($"Order {message.OrderId} was not received yet.");
            }

            orderToShip.State = OrderState.Shipped;
            await orders.Update(orderToShip);
            this.log.Info($"Shipped order {orderToShip.OrderId} to customer {orderToShip.CustomerId}");

            var shippedOrder = new OrderShipped {OrderId = message.OrderId};
            await context.Publish(shippedOrder);
        }
    }
}


