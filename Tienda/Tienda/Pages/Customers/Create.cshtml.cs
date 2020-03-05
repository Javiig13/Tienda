using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda
{
    public class CreateModelCustomer : PageModel
    {
        private readonly IRepository<Customer> _repository;

        public CreateModelCustomer(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repository.Create(Customer);

            return RedirectToPage("./Index");
        }
    }
}
