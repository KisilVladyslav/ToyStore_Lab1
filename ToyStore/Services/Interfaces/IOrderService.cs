using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToyStore.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersByCustomerAsync(Guid customerId);
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Guid orderId);
    }
}