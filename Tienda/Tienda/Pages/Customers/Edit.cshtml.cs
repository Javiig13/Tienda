using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda
{
    public class EditModelCustomer : PageModel
    {
        private readonly IRepository<Customer> _repository;

        public EditModelCustomer(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = _repository.GetById(id.Value);

            if (Customer == null)
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

            _repository.Update(Customer.Id, Customer);

            return RedirectToPage("./Index");
        }
    }
}
