using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class CartService : ICartService
{
    private readonly ToyStoreDbContext _context;

    public CartService(ToyStoreDbContext context)
    {
        _context = context;
    }

    public async Task<Cart> GetCartByCustomerIdAsync(int customerId)
    {
        return await _context.Carts
                             .Include(c => c.CartItems)
                             .ThenInclude(ci => ci.Toy)
                             .FirstOrDefaultAsync(c => c.CustomerId == customerId);
    }

    public async Task AddItemToCartAsync(int customerId, int toyId, int quantity)
    {
        var cart = await GetCartByCustomerIdAsync(customerId);
        if (cart == null)
        {
            cart = new Cart { CustomerId = customerId, CartItems = new List<CartItem>() };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        var cartItem = new CartItem { CartId = cart.Id, ToyId = toyId, Quantity = quantity };
        _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveItemFromCartAsync(int customerId, int toyId)
    {
        var cart = await GetCartByCustomerIdAsync(customerId);
        if (cart != null)
        {
            var cartItem = await _context.CartItems
                                          .FirstOrDefaultAsync(ci => ci.CartId == cart.Id && ci.ToyId == toyId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }
    }

    public async Task UpdateCartItemAsync(int customerId, int toyId, int quantity)
    {
        var cart = await GetCartByCustomerIdAsync(customerId);
        if (cart != null)
        {
            var cartItem = await _context.CartItems
                                          .FirstOrDefaultAsync(ci => ci.CartId == cart.Id && ci.ToyId == toyId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _context.CartItems.Update(cartItem);
                await _context.SaveChangesAsync();
            }
        }
    }

    public async Task ClearCartAsync(int customerId)
    {
        var cart = await GetCartByCustomerIdAsync(customerId);
        if (cart != null)
        {
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();
        }
    }
}
