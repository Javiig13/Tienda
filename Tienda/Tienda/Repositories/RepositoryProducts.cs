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

        public IEnumerable<Product> ObtenerTodos()
        {
            return _dbContext.Products.ToList();
        }

        public Product ObtenerPorId(int id)
        {
            return _dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public void Crear(Product producto)
        {
            _dbContext.Products.Add(producto);
            _dbContext.SaveChanges();
        }

        public void Actualizar(int id, Product productoNuevo)
        {
            Product productoAntiguo = _dbContext.Products.FirstOrDefault(p => p.Id == id);

            productoAntiguo.Nombre = productoNuevo.Nombre;
            productoAntiguo.Precio = productoNuevo.Precio;
            productoAntiguo.Stock = productoNuevo.Stock;
            productoAntiguo.Imagen = productoNuevo.Imagen;

            _dbContext.SaveChanges();
        }

        public void Eliminar(Product producto)
        {
            _dbContext.Products.Remove(producto);
            _dbContext.SaveChanges();
        }
    }
}
