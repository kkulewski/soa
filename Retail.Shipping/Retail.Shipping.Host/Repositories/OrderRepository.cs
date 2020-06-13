namespace Retail.Shipping.Host.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Retail.Shipping.Host.Models;

    public class OrderRepository : IOrderRepository
    {
        private static readonly List<Order> orders = new List<Order>();

        public async Task<List<Order>> GetAll()
        {
            return await Task.FromResult(orders);
        }

        public async Task<Order> GetById(string orderId)
        {
            var order = orders.FirstOrDefault(order => order.OrderId == orderId);
            return await Task.FromResult(order);
        }

        public Task Add(Order newOrder)
        {
            if (!orders.Exists(existingOrder => existingOrder.OrderId == newOrder.OrderId))
            {
                orders.Add(newOrder);
            }

            return Task.CompletedTask;
        }

        public Task Update(Order updatedOrder)
        {
            var orderToUpdate = orders.FirstOrDefault(ord => ord.OrderId == updatedOrder.OrderId);

            if (orderToUpdate == null)
            {
                throw new Exception($"Order {updatedOrder.OrderId} does not exist!");
            }

            orderToUpdate.CustomerId = updatedOrder.CustomerId;
            orderToUpdate.Products = updatedOrder.Products;
            orderToUpdate.State = updatedOrder.State;

            return Task.CompletedTask;
        }
    }
}
