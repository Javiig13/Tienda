using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tienda.DataAccess;
using Tienda.DTOs;
using Tienda.Models;

namespace Tienda.Services
{
    public class ProductOrdersService
    {
        public static async Task CreateAsync(OrderDTO order, DatabaseContext dbContext)
        {
            Customer received = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == order.CustomerId);
            Order toCreate = new Order() { Customer = received, OrderDate = DateTime.UtcNow };

            dbContext.Orders.Add(toCreate);
            dbContext.SaveChanges();

            order.Products.ToList().ForEach(product =>
            {
                dbContext.ProductOrder.Add(new ProductOrder
                {
                    OrderId = toCreate.Id,
                    Order = toCreate,
                    ProductId = product,
                    Product = dbContext.Products.FirstOrDefault(p => p.Id == product)
                });
            });

            dbContext.SaveChanges();

            UpdateStock(toCreate, dbContext);
        }

        internal static void UpdateStock(Order order, DatabaseContext dbContext)
        {
            var productsIds = order.ProductOrders.Select(po => po.ProductId);
            dbContext.Products.Where(x => productsIds.Contains(x.Id)).ToList().ForEach(p => p.Stock -= 1);
            dbContext.SaveChanges();
        }
    }
}
