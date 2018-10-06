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
    [Route("api/ShippingMethods")]
    public class ShippingMethodsController : Controller
    {
        private readonly StoreContext _context;

        public ShippingMethodsController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/ShippingMethods
        [HttpGet]
        public IEnumerable<ShippingMethod> GetShippingMethod()
        {
            return _context.ShippingMethod;
        }

        // GET: api/ShippingMethods/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShippingMethod([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shippingMethod = await _context.ShippingMethod.SingleOrDefaultAsync(m => m.Id == id);

            if (shippingMethod == null)
            {
                return NotFound();
            }

            return Ok(shippingMethod);
        }

        // PUT: api/ShippingMethods/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShippingMethod([FromRoute] int id, [FromBody] ShippingMethod shippingMethod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shippingMethod.Id)
            {
                return BadRequest();
            }

            _context.Entry(shippingMethod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingMethodExists(id))
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

        // POST: api/ShippingMethods
        [HttpPost]
        public async Task<IActionResult> PostShippingMethod([FromBody] ShippingMethod shippingMethod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ShippingMethod.Add(shippingMethod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShippingMethod", new { id = shippingMethod.Id }, shippingMethod);
        }

        // DELETE: api/ShippingMethods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingMethod([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shippingMethod = await _context.ShippingMethod.SingleOrDefaultAsync(m => m.Id == id);
            if (shippingMethod == null)
            {
                return NotFound();
            }

            _context.ShippingMethod.Remove(shippingMethod);
            await _context.SaveChangesAsync();

            return Ok(shippingMethod);
        }

        private bool ShippingMethodExists(int id)
        {
            return _context.ShippingMethod.Any(e => e.Id == id);
        }
    }
}
