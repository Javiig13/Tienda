using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tienda.DataAccess;
using Tienda.Models;

namespace Tienda
{
    public class MarketModel : PageModel
    {
        private readonly Tienda.DataAccess.DatabaseContext _context;

        public MarketModel(Tienda.DataAccess.DatabaseContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }
    }
}
