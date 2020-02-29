using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IRepository<Product> _repository;

        public ProductsController(IRepository<Product> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Product> products = _repository.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product product = _repository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            _repository.Create(product);
            return CreatedAtRoute("Get", new { product.Id }, product);
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            Product productToUpdate = _repository.GetById(id);

            if (product == null)
            {
                return BadRequest("Product is null");
            }

            if (productToUpdate == null)
            {
                return NotFound();
            }

            _repository.Update(productToUpdate.Id, product);
            return Ok();
        }
    }
}