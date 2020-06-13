
namespace Retail.Shipping.Host.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Retail.Shipping.Host.Models;

    public interface IOrderRepository
    {
        Task<List<Order>> GetAll();
        Task<Order> GetById(string orderId);
        Task Add(Order newOrder);
        Task Update(Order updatedOrder);
    }
}
