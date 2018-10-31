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

            var shoppingCart = await _context.ShoppingCart.OrderByDescending(c => c.CartTimeStamp)
                                                          .FirstOrDefaultAsync(m => m.UserId == id);
                                      

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
            
            var cartProduct = new ShoppingCartProduct();
            var user = _context.Session.Where(s => s.SessionId.Equals(product.SessionId)).FirstOrDefault();

            var shoppingCart = _context.ShoppingCart.Where(c => c.UserId.Equals(user.UserId))
                                                    .OrderByDescending(c => c.CartTimeStamp)
                                                    .FirstOrDefault() as ShoppingCart;

            // Check if a cart already exists for the customer
            if (shoppingCart != null)
            {
                // Update cart timestamp
                shoppingCart.CartTimeStamp = DateTime.Now;

                // Update cart
                await PutShoppingCart(shoppingCart.Id, shoppingCart);

                // Check cart if the product already exists in the cart
                var match = _context.ShoppingCartProduct.Where(cp => cp.ShoppingCartId.Equals(shoppingCart.Id))
                                                        .Where(p => p.ProductId.Equals(product.Id));

                if (match.Count() > 0 && match.FirstOrDefault().ProductId.Equals(product.Id))
                {
                    // PUT request for product in cart
                    cartProduct = new ShoppingCartProduct();
                    cartProduct = match.FirstOrDefault() as ShoppingCartProduct;
                    cartProduct.Quantity = product.CartQuantity + (int)match.FirstOrDefault().Quantity;
                    var actionResult = new ShoppingCartProductsController(_context).PutShoppingCartProduct(cartProduct.Id, cartProduct);
                    return (IActionResult)actionResult;
                }
                else
                {
                    // Adding a product that does not already exist in cart
                    cartProduct = new ShoppingCartProduct();
                    cartProduct.Quantity = product.CartQuantity;
                    cartProduct.Size = product.Size;
                    cartProduct.Color = product.Color;
                    cartProduct.UnitPrice = product.Price;
                    cartProduct.ShoppingCartId = shoppingCart.Id;
                    cartProduct.ProductId = product.Id;

                    var result = new ShoppingCartProductsController(_context).PostShoppingCartProduct(cartProduct);
                    if (result.Exception == null)
                        return Ok();
                    else
                        return NotFound();
                }

                //var cartProductsController = DependencyResolver.Current.GetService<ShoppingCartProductsController>();
                //cartProductsController.ControllerContext = new ControllerContext(this.Request.RequestContext, cartProductsController);

                //await ShoppingCartProductsController.PostShoppingCartProduct(cartProduct);

                // return NoContent();
            }
            else
            {
                // Create a new shopping cart for the customer
                shoppingCart = new ShoppingCart();
                shoppingCart.CartTimeStamp = DateTime.Now;
                shoppingCart.OrderPlaced = false;
                shoppingCart.UserId = user.UserId;

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.ShoppingCart.Add(shoppingCart);
                await _context.SaveChangesAsync();

                var createResult =  CreatedAtAction("GetShoppingCart", new { id = shoppingCart.Id }, shoppingCart);

                // Adding a product that does not already exist in cart
                cartProduct = new ShoppingCartProduct();
                cartProduct.Quantity = product.CartQuantity;
                cartProduct.Size = product.Size;
                cartProduct.Color = product.Color;
                cartProduct.UnitPrice = product.Price;
                cartProduct.ShoppingCartId = shoppingCart.Id;
                cartProduct.ProductId = product.Id;

                var result = new ShoppingCartProductsController(_context).PostShoppingCartProduct(cartProduct);
                return (IActionResult)result;
            }
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