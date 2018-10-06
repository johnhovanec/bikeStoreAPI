using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bikestoreAPI.Models;
using Microsoft.AspNetCore.Cors;

namespace bikestoreAPI.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Produces("application/json")]
    [Route("api/ShoppingCarts")]
    public class ShoppingCartsController : Controller
    {
        private readonly StoreContext _context;

        public ShoppingCartsController(StoreContext context)
        {
            _context = context;

            if (_context.ShoppingCart.Count() == 0)
            {
                _context.ShoppingCart.Add(new ShoppingCart
                {
                    //CustomerId = 1,
                    //CartTimeStamp = System.DateTime.Now,
                });
                _context.SaveChanges();
            }
        }

        // GET: api/ShoppingCarts
        [HttpGet]
        public IEnumerable<ShoppingCart> GetShoppingCart()
        {
            return _context.ShoppingCart;
        }

        // GET: api/ShoppingCarts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShoppingCart([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shoppingCart = await _context.ShoppingCart.SingleOrDefaultAsync(m => m.UserId == id);

            if (shoppingCart == null)
            {
                return NotFound();
            }

            return Ok(shoppingCart);
        }

        // PUT: api/ShoppingCarts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingCart([FromRoute] int id, [FromBody] ShoppingCart shoppingCart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoppingCart.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingCart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartExists(id))
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

        // POST: api/ShoppingCarts
        [EnableCors("AllowMyOrigin")]
        [HttpPost]
        public async Task<IActionResult> PostShoppingCart([FromBody] ShoppingCart shoppingCart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ShoppingCart.Add(shoppingCart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingCart", new { id = shoppingCart.Id }, shoppingCart);
        }

        // DELETE: api/ShoppingCarts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCart([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shoppingCart = await _context.ShoppingCart.SingleOrDefaultAsync(m => m.Id == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            _context.ShoppingCart.Remove(shoppingCart);
            await _context.SaveChangesAsync();

            return Ok(shoppingCart);
        }

        private bool ShoppingCartExists(int id)
        {
            return _context.ShoppingCart.Any(e => e.Id == id);
        }
    }
}