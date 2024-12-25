using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToyStore.Services.Interfaces
{
    public interface IToyService
    {
        Task<List<Toy>> GetAllToysAsync();
        Task<Toy> GetToyByIdAsync(Guid id);
        Task CreateToyAsync(Toy toy);
        Task UpdateToyAsync(Toy toy);
        Task DeleteToyAsync(Guid id);
    }
}