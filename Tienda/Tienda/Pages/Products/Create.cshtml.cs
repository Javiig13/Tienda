using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda
{
    public class CreateModelProduct : PageModel
    {
        private readonly IRepository<Product> _repository;

        public CreateModelProduct(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repository.Create(Product);

            return RedirectToPage("./Index");
        }
    }
}
