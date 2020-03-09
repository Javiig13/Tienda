using Microsoft.AspNetCore.Mvc;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda.Controllers
{
    public class CartController : Controller
    {
        private readonly Cart _cart;
        private readonly IRepository<Product> _repository;

        public CartController(Cart cart, IRepository<Product> repository)
        {
            _cart = cart;
            _repository = repository;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public void AddProductToCart(int productId)
        {
            var selectedProduct = _repository.GetById(productId);

            if (selectedProduct != null)
            {
                _cart.AddToCart(selectedProduct);
            }
        }

        [HttpDelete]
        [IgnoreAntiforgeryToken]
        public void RemoveProductFromCart(int productId)
        {
            var selectedProduct = _repository.GetById(productId);

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
    }
}
