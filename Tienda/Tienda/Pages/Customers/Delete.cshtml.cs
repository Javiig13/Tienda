using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda
{
    public class DeleteModelCustomer : PageModel
    {
        private readonly IRepository<Customer> _repository;

        public DeleteModelCustomer(IRepository<Customer> repository)
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

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = _repository.GetById(id.Value);

            if (Customer != null)
            {
                _repository.Delete(Customer);
            }

            return RedirectToPage("./Index");
        }
    }
}
