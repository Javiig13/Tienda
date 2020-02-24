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

        public void Create(Product producto)
        {
            _dbContext.Products.Add(producto);
            _dbContext.SaveChanges();
        }

        public void Update(int id, Product productoNuevo)
        {
            Product productoAntiguo = _dbContext.Products.FirstOrDefault(p => p.Id == id);

            productoAntiguo.Name = productoNuevo.Name;
            productoAntiguo.Price = productoNuevo.Price;
            productoAntiguo.Stock = productoNuevo.Stock;
            productoAntiguo.Image = productoNuevo.Image;

            _dbContext.SaveChanges();
        }

        public void Delete(Product producto)
        {
            _dbContext.Products.Remove(producto);
            _dbContext.SaveChanges();
        }
    }
}
