using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bikestoreAPI.Models;

namespace bikestoreAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/OrderProducts")]
    public class OrderProductsController : Controller
    {
        private readonly StoreContext _context;

        public OrderProductsController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/OrderProducts
        [HttpGet]
        public IEnumerable<OrderProduct> GetOrderProduct()
        {
            return _context.OrderProduct;
        }

        // GET: api/OrderProducts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderProduct = await _context.OrderProduct.SingleOrDefaultAsync(m => m.Id == id);

            if (orderProduct == null)
            {
                return NotFound();
            }

            return Ok(orderProduct);
        }

        // PUT: api/OrderProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderProduct([FromRoute] int id, [FromBody] OrderProduct orderProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<IActionResult> PostOrderProduct([FromBody] OrderProduct orderProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.OrderProduct.Add(orderProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = orderProduct.Id }, orderProduct);
        }

        // DELETE: api/OrderProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderProduct = await _context.OrderProduct.SingleOrDefaultAsync(m => m.Id == id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            _context.OrderProduct.Remove(orderProduct);
            await _context.SaveChangesAsync();

            return Ok(orderProduct);
        }

        private bool OrderProductExists(int id)
        {
            return _context.OrderProduct.Any(e => e.Id == id);
        }
    }
}
