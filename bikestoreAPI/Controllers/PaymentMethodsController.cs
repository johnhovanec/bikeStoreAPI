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
    [Route("api/PaymentMethods")]
    public class PaymentMethodsController : Controller
    {
        private readonly StoreContext _context;

        public PaymentMethodsController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/PaymentMethods
        [HttpGet]
        public IEnumerable<PaymentMethod> GetPaymentMethod()
        {
            return _context.PaymentMethod;
        }

        // GET: api/PaymentMethods/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentMethod([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentMethod = await _context.PaymentMethod.SingleOrDefaultAsync(m => m.Id == id);

            if (paymentMethod == null)
            {
                return NotFound();
            }

            return Ok(paymentMethod);
        }

        // PUT: api/PaymentMethods/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentMethod([FromRoute] int id, [FromBody] PaymentMethod paymentMethod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentMethod.Id)
            {
                return BadRequest();
            }

            _context.Entry(paymentMethod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentMethodExists(id))
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

        // POST: api/PaymentMethods
        [HttpPost]
        public async Task<IActionResult> PostPaymentMethod([FromBody] PaymentMethod paymentMethod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PaymentMethod.Add(paymentMethod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentMethod", new { id = paymentMethod.Id }, paymentMethod);
        }

        // DELETE: api/PaymentMethods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentMethod = await _context.PaymentMethod.SingleOrDefaultAsync(m => m.Id == id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            _context.PaymentMethod.Remove(paymentMethod);
            await _context.SaveChangesAsync();

            return Ok(paymentMethod);
        }

        private bool PaymentMethodExists(int id)
        {
            return _context.PaymentMethod.Any(e => e.Id == id);
        }
    }
}
