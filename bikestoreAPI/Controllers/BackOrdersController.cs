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
    [Route("api/BackOrders")]
    public class BackOrdersController : Controller
    {
        private readonly StoreContext _context;

        public BackOrdersController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/BackOrders
        [HttpGet]
        public IEnumerable<BackOrder> GetBackOrder()
        {
            return _context.BackOrder;
        }

        // GET: api/BackOrders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBackOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var backOrder = await _context.BackOrder.SingleOrDefaultAsync(m => m.Id == id);

            if (backOrder == null)
            {
                return NotFound();
            }

            return Ok(backOrder);
        }

        // PUT: api/BackOrders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBackOrder([FromRoute] int id, [FromBody] BackOrder backOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != backOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(backOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BackOrderExists(id))
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

        // POST: api/BackOrders
        [HttpPost]
        public async Task<IActionResult> PostBackOrder([FromBody] BackOrder backOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BackOrder.Add(backOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBackOrder", new { id = backOrder.Id }, backOrder);
        }

        // DELETE: api/BackOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBackOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var backOrder = await _context.BackOrder.SingleOrDefaultAsync(m => m.Id == id);
            if (backOrder == null)
            {
                return NotFound();
            }

            _context.BackOrder.Remove(backOrder);
            await _context.SaveChangesAsync();

            return Ok(backOrder);
        }

        private bool BackOrderExists(int id)
        {
            return _context.BackOrder.Any(e => e.Id == id);
        }
    }
}
