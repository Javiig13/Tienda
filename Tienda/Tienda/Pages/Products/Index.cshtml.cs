using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
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
            Product = new List<ProductWithImage>();
        }

        public List<ProductWithImage> Product { get; set; }

        public void OnGet()
        {
            List<Product> products = _repository.GetAll().ToList();
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

    public class ProductWithImage : Product
    {
        public new string Image { get; set; }
    }
}
