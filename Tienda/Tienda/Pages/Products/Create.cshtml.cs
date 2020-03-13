using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda
{
    public class CreateModelProduct : PageModel
    {
        private readonly IRepository<Product> _repository;
        private readonly IRepository<Customer> _repositoryCustomers;

        public CreateModelProduct(IRepository<Product> repository, IRepository<Customer> repositoryCustomers)
        {
            _repository = repository;
            _repositoryCustomers = repositoryCustomers;
        }

        public IActionResult OnGet()
        {
            bool isLogged = Shared.UserIsLogged(HttpContext.Session);
            bool isAdministrator = Shared.IsAdministrator(HttpContext.Session, _repositoryCustomers);

            if (!isLogged || !isAdministrator)
            {
                return RedirectToPage("../WithoutPermissions");
            }
            return Page();
        }

        [BindProperty]
        public IFormFile Upload { get; set; }
        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product.Image = await Shared.GetBytes(Upload);

            _repository.Create(Product);

            return RedirectToPage("./Index");
        }
    }
}
