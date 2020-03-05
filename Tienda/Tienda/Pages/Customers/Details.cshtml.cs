using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda
{
    public class DetailsModelCustomer : PageModel
    {
        private readonly IRepository<Customer> _repository;

        public DetailsModelCustomer(IRepository<Customer> repository)
        {
            _repository = repository;
        }

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
    }
}
