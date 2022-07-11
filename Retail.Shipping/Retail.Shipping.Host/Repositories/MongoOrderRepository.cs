namespace Retail.Shipping.Host.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Retail.Shipping.Host.Models;

    using MongoDB.Driver;

    public class MongoOrderRepository : IOrderRepository
    {
        private IMongoDatabase database;
        private IMongoCollection<Order> orders;

        public MongoOrderRepository()
        {
            MongoClient client = new MongoClient("mongodb://retail:retail@retail-mongo:27017");
            this.database = client.GetDatabase("retail-shipping");
            this.orders = database.GetCollection<Order>("orders");
        }

        public async Task<List<Order>> GetAll()
        {
            return await this.orders.Find(_ => true).ToListAsync();
        }

        public async Task<Order> GetById(string orderId)
        {
            return await orders.Find(x => x.OrderId == orderId).FirstOrDefaultAsync();
        }

        public async Task Add(Order newOrder)
        {
            await orders.InsertOneAsync(newOrder);
        }

        public async Task Update(Order updatedOrder)
        {
            var orderToUpdate = await this.GetById(updatedOrder.OrderId);

            if (orderToUpdate == null)
            {
                throw new Exception($"Order {updatedOrder.OrderId} does not exist!");
            }

            orderToUpdate.CustomerId = updatedOrder.CustomerId;
            orderToUpdate.Products = updatedOrder.Products;
            orderToUpdate.State = updatedOrder.State;

            await orders.ReplaceOneAsync(x => x.OrderId == orderToUpdate.OrderId, orderToUpdate);
        }
    }
}
