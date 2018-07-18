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
    [Route("api/ShoppingCartProducts")]
    public class ShoppingCartProductsController : Controller
    {
        private readonly StoreContext _context;

        public ShoppingCartProductsController(StoreContext context)
        {
            _context = context;


            if (_context.ShoppingCartProduct.Count() == 0)
            {
                _context.ShoppingCartProduct.Add(new ShoppingCartProduct
                {
                    ShoppingCartId = 1,
                    ProductId = 1,
                    Quantity = 2,
                    Color = "",
                });

                _context.ShoppingCartProduct.Add(new ShoppingCartProduct
                {
                    ShoppingCartId = 1,
                    ProductId = 2,
                    Quantity = 1,
                    Color = "",
                });
                _context.SaveChanges();
            }
        }

        // GET: api/ShoppingCartProducts
        [HttpGet]
        public IEnumerable<ShoppingCartProduct> GetShoppingCartProduct()
        {
            return _context.ShoppingCartProduct;
        }

        // GET: api/ShoppingCartProducts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShoppingCartProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shoppingCartProduct = await _context.ShoppingCartProduct.SingleOrDefaultAsync(m => m.Id == id);

            if (shoppingCartProduct == null)
            {
                return NotFound();
            }

            return Ok(shoppingCartProduct);
        }

        // PUT: api/ShoppingCartProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingCartProduct([FromRoute] int id, [FromBody] ShoppingCartProduct shoppingCartProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoppingCartProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingCartProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartProductExists(id))
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

        // POST: api/ShoppingCartProducts
        [HttpPost]
        public async Task<IActionResult> PostShoppingCartProduct([FromBody] ShoppingCartProduct shoppingCartProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ShoppingCartProduct.Add(shoppingCartProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingCartProduct", new { id = shoppingCartProduct.Id }, shoppingCartProduct);
        }

        // DELETE: api/ShoppingCartProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCartProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shoppingCartProduct = await _context.ShoppingCartProduct.SingleOrDefaultAsync(m => m.Id == id);
            if (shoppingCartProduct == null)
            {
                return NotFound();
            }

            _context.ShoppingCartProduct.Remove(shoppingCartProduct);
            await _context.SaveChangesAsync();

            return Ok(shoppingCartProduct);
        }

        private bool ShoppingCartProductExists(int id)
        {
            return _context.ShoppingCartProduct.Any(e => e.Id == id);
        }
    }
}