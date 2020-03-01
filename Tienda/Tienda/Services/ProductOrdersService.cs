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
            _dbContext.SaveChanges();

            order.Products.ToList().ForEach(product =>
            {
                _dbContext.ProductOrder.Add(new ProductOrder
                {
                    OrderId = toCreate.Id,
                    Order = toCreate,
                    ProductId = product,
                    Product = _dbContext.Products.FirstOrDefault(p => p.Id == product)
                });
            });

            _dbContext.SaveChanges();

            UpdateStock(toCreate);
        }

        public void UpdateStock(Order order)
        {
            var productsIds = order.ProductOrders.Select(po => po.ProductId);
            _dbContext.Products.Where(x => productsIds.Contains(x.Id)).ToList().ForEach(p => p.Stock -= 1);
            _dbContext.SaveChanges();
        }
    }
}
