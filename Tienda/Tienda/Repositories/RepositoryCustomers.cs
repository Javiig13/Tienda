using System.Collections.Generic;
using System.Linq;
using Tienda.DataAccess;
using Tienda.Models;

namespace Tienda.Repositories
{
    public class RepositoryCustomers : IRepository<Customer>
    {
        private readonly DatabaseContext _dbContext;

        public RepositoryCustomers(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _dbContext.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return _dbContext.Customers.FirstOrDefault(p => p.Id == id);
        }

        public void Create(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
        }

        public void Update(int id, Customer newCustomer)
        {
            Customer oldCustomer = _dbContext.Customers.FirstOrDefault(p => p.Id == id);

            oldCustomer.Name = newCustomer.Name;

            _dbContext.SaveChanges();
        }

        public void Delete(Customer customer)
        {
            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();
        }
    }
}