using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ToyService : IToyService
{
    private readonly ToyStoreDbContext _context;

    public ToyService(ToyStoreDbContext context)
    {
        _context = context;
    }

    public async Task<List<Toy>> GetAllToysAsync()
    {
        return await _context.Toys.Include(t => t.Category).ToListAsync();
    }

    public async Task<Toy> GetToyByIdAsync(int id)
    {
        return await _context.Toys.Include(t => t.Category)
                                  .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task CreateToyAsync(Toy toy)
    {
        _context.Toys.Add(toy);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateToyAsync(Toy toy)
    {
        _context.Toys.Update(toy);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteToyAsync(int id)
    {
        var toy = await GetToyByIdAsync(id);
        if (toy != null)
        {
            _context.Toys.Remove(toy);
            await _context.SaveChangesAsync();
        }
    }
}
