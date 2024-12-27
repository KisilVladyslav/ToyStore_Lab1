using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToyStore.Services.Interfaces;

namespace ToyStore.Services
{
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

        public async Task<Toy> GetToyByIdAsync(Guid id)
        {
            return await _context.Toys.Include(t => t.Category)
                                      .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Toy> CreateToyAsync(Toy toy)
        {
            _context.Toys.Add(toy);
            await _context.SaveChangesAsync();
            return toy;
        }

        public async Task UpdateToyAsync(Toy toy)
        {
            _context.Toys.Update(toy);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteToyAsync(Guid id)
        {
            var toy = await GetToyByIdAsync(id);
            if (toy != null)
            {
                _context.Toys.Remove(toy);
                await _context.SaveChangesAsync();
            }
        }
    }
}