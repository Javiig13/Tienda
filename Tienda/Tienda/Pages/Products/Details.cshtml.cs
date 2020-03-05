using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public Product Product { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = _repository.GetById(id.Value);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
