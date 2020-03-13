using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda
{
    public class EditModelProduct : PageModel
    {
        private readonly IRepository<Product> _repository;
        private readonly IRepository<Customer> _repositoryCustomers;

        public EditModelProduct(IRepository<Product> repository, IRepository<Customer> repositoryCustomers)
        {
            _repository = repository;
            _repositoryCustomers = repositoryCustomers;
        }

        [BindProperty]
        public IFormFile Upload { get; set; }
        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public string PreviousImage { get; set; }

        public IActionResult OnGet(int? id)
        {
            bool isLogged = Shared.UserIsLogged(HttpContext.Session);
            bool isAdministrator = Shared.IsAdministrator(HttpContext.Session, _repositoryCustomers);

            if (!isLogged || !isAdministrator)
            {
                return RedirectToPage("../WithoutPermissions");
            }

            if (id == null)
            {
                return NotFound();
            }

            Product = _repository.GetById(id.Value);
            PreviousImage = "data:image/png;base64, " + Convert.ToBase64String(Product.Image);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product.Image = await Shared.GetBytes(Upload);
            _repository.Update(Product.Id, Product);

            return RedirectToPage("./Index");
        }
    }
}
