using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToyStore.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();
        //Task<Customer> GetCustomerByIdAsync(Guid id);
        Task CreateCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        //Task DeleteCustomerAsync(Guid id);
    }
}