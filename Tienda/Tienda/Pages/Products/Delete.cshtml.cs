using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda
{
    public class DeleteModelProduct : PageModel
    {
        private readonly IRepository<Product> _repository;

        public DeleteModelProduct(IRepository<Product> repository)
        {
            _repository = repository;
        }

        [BindProperty]
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

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = _repository.GetById(id.Value);

            if (Product != null)
            {
                _repository.Delete(Product);
            }

            return RedirectToPage("./Index");
        }
    }
}
