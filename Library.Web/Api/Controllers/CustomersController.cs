using Library.Database;
using Library.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Api.Controllers
{
    [Route("api/[controller]")] // {bazowyAdresAplikacji}/api/customers
    public class CustomersController : Controller
    {
        private readonly CustomersRepository _customersRepository = new CustomersRepository();

        /// <summary>
        /// Returns all customers
        /// </summary>
        /// <returns>All customers in the db</returns>
        [HttpGet]
        [Route("")] // {bazowyAdresAplikacji}/api/customers GET
        public IEnumerable<Customer> Get()
        {
            return _customersRepository.GetAll();
        }

        [HttpGet]
        [Route("{id}")] // np {bazowyAdresAplikacji}/api/customers/1 GET
        public IActionResult Get([FromRoute]int id)
        {
            var customer = _customersRepository.Get(id);
            if (customer != null)
                return Ok(customer);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody]Customer customer)
        {
            return Created("", _customersRepository.Add(customer));
        }

        [HttpPut]
        [Route("")]
        public IActionResult Update([FromBody]Customer customer)
        {
            _customersRepository.Update(customer);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            _customersRepository.Delete(id);
            return Ok();
        }
    }
}
