using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tienda.DataAccess;
using Tienda.Models;

namespace Tienda
{
    public class DetailsModel : PageModel
    {
        private readonly DatabaseContext _context;

        public DetailsModel(DatabaseContext context)
        {
            _context = context;
        }

        public IList<ProductDetail> Products { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Products = await _context.ProductOrder.Where(m => m.OrderId == id).Select(c => new ProductDetail()
            {
                Amount = c.Amount,
                Image = "data:image/png;base64, " + Convert.ToBase64String(c.Product.Image),
                Name = c.Product.Name,
                Price = c.Product.Price,
                TotalPrice = c.Amount * c.Product.Price

            }).ToListAsync();

            if (Products == null)
            {
                return NotFound();
            }
            return Page();
        }
    }

    public class ProductDetail : Product
    {
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
        public new string Image { get; set; }
    }
}
