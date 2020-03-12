using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tienda.DTOs;
using Tienda.Models;
using Tienda.Repositories;
using Tienda.Services;

namespace Tienda.Controllers
{
    public class CartController : Controller
    {
        private readonly Cart _cart;
        private readonly IRepository<Product> _repositoryProducts;
        private readonly IRepository<Customer> _repositoryCustomers;
        private readonly IProductOrdersService _productOrdersService;

        public CartController(Cart cart, IRepository<Product> repositoryProducts, IRepository<Customer> repositoryCustomers, IProductOrdersService productOrdersService)
        {
            _cart = cart;
            _repositoryProducts = repositoryProducts;
            _repositoryCustomers = repositoryCustomers;
            _productOrdersService = productOrdersService;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public void AddProductToCart(int productId)
        {
            var selectedProduct = _repositoryProducts.GetById(productId);

            if (selectedProduct != null)
            {
                _cart.AddToCart(selectedProduct);
            }
        }

        [HttpDelete]
        [IgnoreAntiforgeryToken]
        public void RemoveProductFromCart(int productId)
        {
            var selectedProduct = _repositoryProducts.GetById(productId);

            if (selectedProduct != null)
            {
                _cart.RemoveFromCart(selectedProduct);
            }
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult ClearCart()
        {
            _cart.ClearCart();
            return RedirectToPage("/Market/Market");
        }

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> CompleteBuy()
        {
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                return RedirectToPage("/Login/Login");
            }

            Customer customer = _repositoryCustomers.GetById(int.Parse(HttpContext.Session.GetString("UserSession")));

            if (customer == null)
            {
                return BadRequest();
            }

            OrderDTO order = new OrderDTO()
            {
                CustomerId = customer.Id,
                Products = _cart.GetCartItems()
            };

            await _productOrdersService.CreateAsync(order);

            return RedirectToPage("/Orders/Index");
        }
    }
}
