using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda
{
    public class IndexModelCustomer : PageModel
    {
        private readonly IRepository<Customer> _repository;

        public IndexModelCustomer(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public IList<Customer> Customer { get;set; }

        public void OnGet()
        {
            Customer = _repository.GetAll().ToList();
        }
    }
}