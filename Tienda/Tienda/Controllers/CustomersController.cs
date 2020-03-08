using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tienda.Models;
using Tienda.Repositories;

namespace Tienda.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IRepository<Customer> _repository;

        public CustomersController(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    IEnumerable<Customer> customers = _repository.GetAll();
        //    return Ok(customers);
        //}

        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    Customer customer = _repository.GetById(id);

        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(customer);
        //}

        //[HttpPost]
        //public IActionResult Post([FromBody]Customer customer)
        //{
        //    if (customer == null)
        //    {
        //        return BadRequest();
        //    }
        //    _repository.Create(customer);
        //    return CreatedAtRoute("Get", new { customer.Id }, customer);
        //}

        //[HttpPut]
        //public IActionResult Put(int id, [FromBody]Customer customer)
        //{
        //    Customer customerToUpdate = _repository.GetById(id);

        //    if (customer == null)
        //    {
        //        return BadRequest("Customer is null");
        //    }

        //    if (customerToUpdate == null)
        //    {
        //        return NotFound();
        //    }

        //    _repository.Update(customerToUpdate.Id, customer);
        //    return NotFound();
        //}

        [HttpGet]
        public IActionResult Register()
        {
            return new RedirectToPageResult("/Customers/Create");
        }
    }
}
