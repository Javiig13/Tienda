using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Tienda.Models;

namespace Tienda
{
    public class CartItemsModel : PageModel
    {
        private readonly Cart _cart;

        public IList<CartItem> CartItems { get; set; }

        public CartItemsModel(Cart cart)
        {
            _cart = cart;
        }

        public void OnGet()
        {
            var items = _cart.GetCartItems();
            _cart.CartItems = items;
            CartItems = items;
        }
    }
}