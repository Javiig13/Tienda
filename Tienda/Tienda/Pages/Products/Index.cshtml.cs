using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda
{
    public class IndexModelProduct : PageModel
    {
        private readonly IRepository<Product> _repository;

        public IndexModelProduct(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public IList<Product> Product { get;set; }

        public void OnGet()
        {
            Product = _repository.GetAll().ToList();
        }
    }
}
