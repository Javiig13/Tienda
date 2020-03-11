using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tienda.DataAccess;
using Tienda.DTOs;
using Tienda.Models;

namespace Tienda.Services
{
    public class ProductOrdersService : IProductOrdersService
    {
        private readonly DatabaseContext _dbContext;

        public ProductOrdersService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(OrderDTO order)
        {
            Customer received = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == order.CustomerId);
            Order toCreate = new Order() { Customer = received, OrderDate = DateTime.UtcNow };

            _dbContext.Orders.Add(toCreate);
            await _dbContext.SaveChangesAsync();

            order.Products.ToList().ForEach(product =>
            {
                var productOrder = new ProductOrder
                {
                    OrderId = toCreate.Id,
                    Order = toCreate,
                    ProductId = product.Product.Id,
                    Product = _dbContext.Products.FirstOrDefault(p => p.Id == product.Product.Id),
                    Amount = product.Amount
                };
                _dbContext.ProductOrder.Add(productOrder);
                UpdateStock(productOrder);
            });

            await _dbContext.SaveChangesAsync();
        }

        public void UpdateStock(ProductOrder order)
        {
            var actualStock = _dbContext.Products.Where(p => p.Id == order.Product.Id).FirstOrDefault().Stock;
            _dbContext.Products.Where(p => p.Id == order.Product.Id).FirstOrDefault().Stock = actualStock - order.Amount;
            _dbContext.SaveChanges();
        }
    }
}
