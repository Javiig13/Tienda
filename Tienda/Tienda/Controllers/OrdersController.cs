using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Tienda.DataAccess;
using Tienda.DTOs;
using Tienda.Services;

namespace Tienda.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly DatabaseContext _dbContext;
        public OrdersController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var productOrders = _dbContext.ProductOrder.ToList();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrderDTO order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            await ProductOrdersService.CreateAsync(order, _dbContext);
            return CreatedAtRoute("Get", new { order.Id }, order);
        }
    }
}