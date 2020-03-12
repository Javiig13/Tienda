using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tienda.DataAccess;

namespace Tienda
{
    public class IndexModel : PageModel
    {
        private readonly DatabaseContext _context;

        public IndexModel(DatabaseContext context)
        {
            _context = context;
        }

        public IList<OrderDetail> OrderDetails { get; set; }

        public async Task OnGetAsync()
        {
            List<OrderDetail> details = new List<OrderDetail>();

            var userId = int.Parse(HttpContext.Session.GetString("UserSession"));

            var orders = await _context.ProductOrder
                .Include(p => p.Order)
                .Include(p => p.Product)
                .Where(x => x.Order.Customer.Id == userId)
                .ToListAsync();

            var result = orders.GroupBy(x => x.Order.Id).Select(g => (
                OrderId: g.Key,
                OrderDate: g.Select(c => c.Order.OrderDate).FirstOrDefault(),
                OrderPrice: g.Sum(x => x.Amount > 0 ? x.Amount : x.Amount)
            )).ToList();

            result.ForEach(r =>
            {
                details.Add(new OrderDetail()
                {
                    OrderDate = r.OrderDate.Day + "/" + r.OrderDate.Month + "/" + r.OrderDate.Year,
                    OrderId = r.OrderId,
                    OrderPrice = r.OrderPrice
                });
            });

            OrderDetails = details;
        }
    }

    public class OrderDetail
    {
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public decimal OrderPrice { get; set; }
    }
}
