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
    public class AddressController : ControllerBase
    {
        StoreContext db;
        public AddressController(StoreContext context)
        {
            db = context;
            if (!db.Addresses.Any())
            {
                db.Addresses.Add(new Address { Index = 456987, City = "Saint-Petersburg", Street= "Moika", House= "3A", Room="1408" });
                db.SaveChanges();
            }
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> Get()
        {
            return await db.Addresses.ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> Get(int id)
        {
            Address address = await db.Addresses.FirstOrDefaultAsync(x => x.Id == id);
            if (address == null)
                return NotFound();
            return new ObjectResult(address);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<Address>> Post(Address address)
        {
            if (address == null)
            {
                return BadRequest();
            }

            db.Addresses.Add(address);
            await db.SaveChangesAsync();
            return Ok(address);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Address>> Put(Address address)
        {
            if (address == null)
            {
                return BadRequest();
            }
            if (!db.Addresses.Any(x => x.Id == address.Id))
            {
                return NotFound();
            }

            db.Update(address);
            await db.SaveChangesAsync();
            return Ok(address);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> Delete(int id)
        {
            Address address = db.Addresses.FirstOrDefault(x => x.Id == id);
            if (address == null)
            {
                return NotFound();
            }
            db.Addresses.Remove(address);
            await db.SaveChangesAsync();
            return Ok(address);
        }
    }
}
