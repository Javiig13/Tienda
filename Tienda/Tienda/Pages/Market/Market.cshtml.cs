using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tienda.DataAccess;
using Tienda.Models;

namespace Tienda
{
    public class MarketModel : PageModel
    {
        private readonly DatabaseContext _context;

        public MarketModel(DatabaseContext context)
        {
            _context = context;
            Product = new List<ProductWithImage>();
        }

        public List<ProductWithImage> Product { get; set; }

        public async Task OnGetAsync()
        {
            List<Product> products = await _context.Products.ToListAsync();
            products.ForEach(p =>
            {
                Product.Add(new ProductWithImage()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Image = "data:image/png;base64, " + Convert.ToBase64String(p.Image)
                });
            });
        }
    }

    public class ProductWithImage : Product
    {
        public new string Image { get; set; }
    }
}
