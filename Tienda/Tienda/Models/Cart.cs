using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Tienda.DataAccess;

namespace Tienda.Models
{
    public class Cart
    {
        private readonly DatabaseContext _dbContext;
        public Cart(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Key]
        public string Id { get; set; }
        public List<CartItem> CartItems { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<DatabaseContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new Cart(context) { Id = cartId };
        }

        public void AddToCart(Product product)
        {
            var item = _dbContext.CartItems.SingleOrDefault
                (c => c.Product.Id == product.Id && c.CartId == Id);

            if (item == null)
            {
                _dbContext.CartItems.Add(new CartItem()
                {
                    CartId = Id,
                    Product = product,
                    Amount = 1
                });
            }
            else
            {
                item.Amount += 1;
            }
            _dbContext.SaveChanges();
        }
        public int RemoveFromCart(Product product)
        {
            var item = _dbContext.CartItems.SingleOrDefault(c => c.Product.Id == product.Id && c.CartId == Id);

            int localAmount = 0;

            if (item != null)
            {
                if (item.Amount > 1)
                {
                    item.Amount--;
                    localAmount = item.Amount;
                }
                else
                {
                    _dbContext.CartItems.Remove(item);
                }
            }

            _dbContext.SaveChanges();

            return localAmount;
        }

        public List<CartItem> GetCartItems()
        {
            return _dbContext.CartItems.Where(c => c.CartId == Id).Include(p => p.Product).ToList();
        }

        public void ClearCart()
        {
            var items = _dbContext.CartItems.Where(c => c.CartId == Id);
            _dbContext.CartItems.RemoveRange(items);
            _dbContext.SaveChanges();
        }
    }

    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CartId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}
