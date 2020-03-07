using System.Linq;
using System.Threading.Tasks;
using Tienda.DataAccess;
using Tienda.Models;

namespace Tienda.Repositories
{
    public class RepositoryIdentity : IIdentityRepository
    {
        private readonly DatabaseContext _dbContext;

        public RepositoryIdentity(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Customer UserExists(Customer customer)
        {
            return _dbContext.Customers.FirstOrDefault(c => c.Username == customer.Username && c.Password == customer.Password);
        }

        public async Task<Customer> RegisterAsync(Customer customer)
        {
            _dbContext.Customers.Add(customer);

            await _dbContext.SaveChangesAsync();

            return customer;
        }
    }
}
