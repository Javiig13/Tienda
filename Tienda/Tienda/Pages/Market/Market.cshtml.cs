using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tienda.DataAccess;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda
{
    public class MarketModel : PageModel
    {
        private readonly IRepository<Product> _repository;

        public MarketModel(IRepository<Product> repository)
        {
            _repository = repository;
            Product = new List<ProductWithImage>();
        }

        public List<ProductWithImage> Product { get; set; }

        public void OnGetAsync()
        {
            List<Product> products = _repository.GetAll().Where(p => p.Stock > 0).ToList();
            products.ForEach(p =>
            {
                Product.Add(new ProductWithImage()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Image = "data:image/png;base64, " + Convert.ToBase64String(p.Image)
                });
            });
        }
    }
}
