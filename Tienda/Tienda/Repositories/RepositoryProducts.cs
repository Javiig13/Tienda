using System;
using System.Collections.Generic;
using System.Linq;
using Tienda.DataAccess;
using Tienda.Models;

namespace Tienda.Repositories
{
    public class RepositoryProducts : IRepository<Product>
    {
        private readonly DatabaseContext _dbContext;

        public RepositoryProducts(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<Product> GetAll()
        {
            return _dbContext.Products.ToList();
        }

        public Product GetById(int id)
        {
            return _dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public void Create(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void Update(int id, Product newProduct)
        {
            Product oldProduct = _dbContext.Products.FirstOrDefault(p => p.Id == id);

            oldProduct.Name = newProduct.Name;
            oldProduct.Price = newProduct.Price;
            oldProduct.Stock = newProduct.Stock;
            oldProduct.Image = newProduct.Image;

            _dbContext.SaveChanges();
        }

        public void Delete(Product product)
        {
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }
    }
}
