using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda
{
    public class DetailsModelProduct : PageModel
    {
        private readonly IRepository<Product> _repository;

        public DetailsModelProduct(IRepository<Product> repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public string Image { get; set; }

        public Product Product { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = _repository.GetById(id.Value);
            Image = "data:image/png;base64, " + Convert.ToBase64String(Product.Image);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
