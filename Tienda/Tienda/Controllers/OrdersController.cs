using Microsoft.AspNetCore.Mvc;
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
        private readonly IProductOrdersService _productOrdersService;
        public OrdersController(DatabaseContext dbContext, IProductOrdersService productOrdersService)
        {
            _dbContext = dbContext;
            _productOrdersService = productOrdersService;
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

            await _productOrdersService.CreateAsync(order);
            return CreatedAtRoute("Get", new { order.Id }, order);
        }
    }
}