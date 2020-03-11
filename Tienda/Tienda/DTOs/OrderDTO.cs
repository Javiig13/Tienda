using System.Collections.Generic;
using Tienda.Models;

namespace Tienda.DTOs
{
    public class OrderDTO
    {
        public int? Id { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<CartItem> Products { get; set; }
    }
}
