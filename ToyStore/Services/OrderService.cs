using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToyStore.Services.Interfaces;

namespace ToyStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly ToyStoreDbContext _context;

        public OrderService(ToyStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByCustomerAsync(int customerId)
        {
            return await _context.Orders
                                 .Include(o => o.OrderItems)
                                 .ThenInclude(oi => oi.Toy)
                                 .Where(o => o.CustomerId == customerId)
                                 .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                                 .Include(o => o.OrderItems)
                                 .ThenInclude(oi => oi.Toy)
                                 .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}