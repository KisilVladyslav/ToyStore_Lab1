using System.Threading.Tasks;

namespace ToyStore.Services.Interfaces
{ 
    public interface ICartService
    {
        Task<Cart> GetCartByCustomerIdAsync(int customerId);
        Task AddItemToCartAsync(int customerId, int toyId, int quantity);
        Task RemoveItemFromCartAsync(int customerId, int toyId);
        Task UpdateCartItemAsync(int customerId, int toyId, int quantity);
        Task ClearCartAsync(int customerId);
    }
}