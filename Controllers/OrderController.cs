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
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        StoreContext db;
        public OrderController(StoreContext context)
        {
            db = context;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return await db.Orders.Include(c => c.Customer).Include(d => d.DeliveryAddress).ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            Order order = await db.Orders.Include(c => c.Customer).Include(d => d.DeliveryAddress).FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
                return NotFound();
            return new ObjectResult(order);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<Order>> Post(int customerID, int addressID)
        {
            Order order = new Order();
            order.Status = Enums.OrderStatus.NotDone;
            order.OrderDateTime = DateTime.Now;


            var customer = db.Customers.FirstOrDefault(x => x.Id == customerID);
            var address = db.Addresses.FirstOrDefault(x => x.Id == addressID);

            if (customer == null || address == null)
                return BadRequest();

            order.Customer = customer;
            order.DeliveryAddress = address;

            db.Orders.Add(order);
            await db.SaveChangesAsync();
            return Ok(order);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> Put(int id, Enums.OrderStatus status)
        {
            Order order = await db.Orders.Include(c => c.Customer).Include(d => d.DeliveryAddress).FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
                return NotFound();

            if (!Enum.IsDefined(typeof(Enums.OrderStatus), status))
                return BadRequest();

            order.Status = status;
            db.Update(order);
            await db.SaveChangesAsync();
            return Ok(order);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> Delete(int id)
        {
            Order Order = db.Orders.FirstOrDefault(x => x.Id == id);
            if (Order == null)
            {
                return NotFound();
            }
            db.Orders.Remove(Order);
            await db.SaveChangesAsync();
            return Ok(Order);
        }
    }
}
