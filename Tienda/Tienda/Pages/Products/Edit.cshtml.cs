using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda
{
    public class EditModelProduct : PageModel
    {
        private readonly IRepository<Product> _repository;

        public EditModelProduct(IRepository<Product> repository)
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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repository.Update(Product.Id, Product);

            return RedirectToPage("./Index");
        }
    }
}
