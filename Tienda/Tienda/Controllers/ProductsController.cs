using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IRepository<Product> _repositorio;

        public ProductsController(IRepository<Product> repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Product> productos = _repositorio.ObtenerTodos();
            return Ok(productos);
        }

        //[HttpGet]
        //public IActionResult Get(int id)
        //{
        //    Product producto = _repositorio.ObtenerPorId(id);

        //    if (producto == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(producto);
        //}

        [HttpPost]
        public IActionResult Post([FromBody]Product producto)
        {
            if (producto == null)
            {
                return BadRequest();
            }
            _repositorio.Crear(producto);
            return CreatedAtRoute("Get", new { producto.Id }, producto);
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody]Product producto)
        {
            Product productoAactualizar = _repositorio.ObtenerPorId(id);

            if (producto == null)
            {
                return BadRequest("Customer is null");
            }

            if (productoAactualizar == null)
            {
                return NotFound();
            }

            _repositorio.Actualizar(productoAactualizar.Id, producto);
            return NotFound();
        }
    }
}
