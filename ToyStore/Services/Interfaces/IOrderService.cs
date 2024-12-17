using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOrderService
{
    Task<List<Order>> GetOrdersByCustomerAsync(int customerId);
    Task<Order> GetOrderByIdAsync(int orderId);
    Task CreateOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(int orderId);
}
