using System.Threading.Tasks;
using Tienda.Models;

namespace Tienda.Repositories
{
    public interface IIdentityRepository
    {
        Customer UserExists(Customer customer);

        Task<Customer> RegisterAsync(Customer customer);
    }
}
