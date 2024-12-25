using System.Threading.Tasks;

namespace ToyStore.Services.Interfaces
{ 
    public interface ICartService
    {
        Task<Cart> GetCartByCustomerIdAsync(Guid customerId);
        Task AddItemToCartAsync(Guid customerId, Guid toyId, int quantity);
        Task RemoveItemFromCartAsync(Guid customerId, Guid toyId);
        Task UpdateCartItemAsync(Guid customerId, Guid toyId, int quantity);
        Task ClearCartAsync(Guid customerId);
    }
}