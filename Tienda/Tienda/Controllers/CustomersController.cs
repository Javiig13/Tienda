using Microsoft.AspNetCore.Mvc;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IRepository<Customer> _repository;

        public CustomersController(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return new RedirectToPageResult("/Customers/Create");
        }
    }
}
