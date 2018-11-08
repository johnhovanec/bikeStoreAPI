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
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        private readonly StoreContext _context;

        public OrdersController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public IEnumerable<Order> GetOrder()
        {
            return _context.Order;
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromRoute] int id, [FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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
        public async Task<IActionResult> PostOrder([FromBody] CartProduct productInCart)
        {
            var shoppingCart = await _context.ShoppingCart.FirstOrDefaultAsync(c => c.Id.Equals(productInCart.CartId)) as ShoppingCart;

            // Check cart has not already placed order
            if (!(bool)shoppingCart.OrderPlaced)
            {
                // Create a new order
                var order = new Order();
                order.TimeStamp = DateTime.Now;

                _context.Order.Add(order);
                await _context.SaveChangesAsync();



                // Add Cart products to a new OrderProduct and save
                foreach (var product in productInCart.cartProducts)
                {
                    var orderProduct = new OrderProduct()
                    {
                        Id = product.Id,
                        Quantity = product.Quantity,
                        Size = product.Size,
                        Color = product.Color,
                        UnitPrice = product.UnitPrice,
                        OrderId = order.Id,
                        Order = order
                    };

                    _context.OrderProduct.Add(orderProduct);
                    await _context.SaveChangesAsync();
                }

                // Update shoppingCart to show order has been placed
                if (!shoppingCart.OrderPlaced == true)
                    return BadRequest();

                shoppingCart.OrderPlaced = true;
                _context.Entry(shoppingCart).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return CreatedAtAction("GetOrder", new { id = order.Id }, order);
            }
            return NotFound();
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }

        public class CartProduct
        {
            public int CartId { get; set; }
            public string FName { get; set; }
            public string MName { get; set; }
            public string LName { get; set; }
            public string Email { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
            public string ShippingSameAsBilling { get; set; }
            public string CardType { get; set; }
            public string NameOnCard { get; set; }
            public string CardNumber { get; set; }
            public string CardExpiration { get; set; }
            public string CardCVV { get; set; }
            public string SourceCode { get; set; }
            public List<ShoppingCartProduct> cartProducts { get; set; }
            public string cartTotal { get; set; }
            public string sessionId { get; set; }
        }
    }
}