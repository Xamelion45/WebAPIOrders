using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI01.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        StoreContext db;
        public CustomerController(StoreContext context)
        {
            db = context;
            if (!db.Customers.Any())
            {
                db.Customers.Add(new Customer {Name = "Lebovskiy", SecondName = "Big", PhoneNumber = "+19994448989" });
                db.SaveChanges();
            }
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            return await db.Customers.ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            Customer Customer = await db.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (Customer == null)
                return NotFound();
            return new ObjectResult(Customer);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<Customer>> Post(Customer Customer)
        {
            if (Customer == null)
            {
                return BadRequest();
            }

            db.Customers.Add(Customer);
            await db.SaveChangesAsync();
            return Ok(Customer);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> Put(Customer Customer)
        {
            if (Customer == null)
            {
                return BadRequest();
            }
            if (!db.Customers.Any(x => x.Id == Customer.Id))
            {
                return NotFound();
            }

            db.Update(Customer);
            await db.SaveChangesAsync();
            return Ok(Customer);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(int id)
        {
            Customer Customer = db.Customers.FirstOrDefault(x => x.Id == id);
            if (Customer == null)
            {
                return NotFound();
            }
            db.Customers.Remove(Customer);
            await db.SaveChangesAsync();
            return Ok(Customer);
        }
    }
}
