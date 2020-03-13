﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda
{
    public class DeleteModelProduct : PageModel
    {
        private readonly IRepository<Product> _repository;
        private readonly IRepository<Customer> _repositoryCustomers;

        public DeleteModelProduct(IRepository<Product> repository, IRepository<Customer> repositoryCustomers)
        {
            _repository = repository;
            _repositoryCustomers = repositoryCustomers;
        }

        [BindProperty]
        public string Image { get; set; }

        [BindProperty]
        public Product Product { get; set; }

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
            Image = "data:image/png;base64, " + Convert.ToBase64String(Product.Image);

            if (Product == null)
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

            Product = _repository.GetById(id.Value);

            if (Product != null)
            {
                _repository.Delete(Product);
            }

            return RedirectToPage("./Index");
        }
    }
}
