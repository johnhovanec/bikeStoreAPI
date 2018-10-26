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
        public async Task<IActionResult> PostShoppingCart([FromBody] AddToCartProduct product)
        {
            //var userId = from u in _context.Session.Where(s => s.SessionId.Equals(product.SessionId))
            //             select u.UserId;

            //var user = users.Where(u => u.Username.Equals(login.Username)).FirstOrDefault();

            var userId = _context.Session.FirstOrDefault(s => s.Id == Convert.ToInt32(product.SessionId));

            var shoppingCart = _context.ShoppingCart.Where(c => c.UserId.Equals(userId))
                                                    .OrderByDescending(c => c.CartTimeStamp)
                                                    .FirstOrDefault() as ShoppingCart;

            // Check if a cart already exists for the customer
            if (shoppingCart != null)
            {
                // Do something
            }
            else
            {
                shoppingCart = new ShoppingCart();
                shoppingCart.CartTimeStamp = DateTime.Now;
                shoppingCart.OrderPlaced = false;
                shoppingCart.UserId = Convert.ToInt32(userId);
            }


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

        // Model to use for AddToCart submit
        public class AddToCartProduct
        {
            public int Id { get; set; }
            public string Manufacturer { get; set; }
            public string Model { get; set; }
            public string Type { get; set; }
            public string Size { get; set; }
            public string Color { get; set; }
            public string Description { get; set; }
            public string Rating { get; set; }
            public decimal Price { get; set; }
            public int InventoryQuantity { get; set; }
            public int CartQuantity { get; set; }
            public string SessionId { get; set; }
        }
    }
}