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
        [HttpGet("~/api/orders/confirmation")]
        public IEnumerable<OrderItems> GetOrder([FromQuery] string Id)
        {
            // Get the order
            var order = _context.Order.SingleOrDefault(m => m.Id == Int32.Parse(Id));
            if (order == null)
                return Enumerable.Empty<OrderItems>();

            var prods =
                from op in _context.OrderProduct
                join prod in _context.Product on op.ProductId equals prod.Id
                where op.OrderId == order.Id
                select new OrderItems
                {
                    Id = op.Id,
                    CartId = (int)order.ShoppingCartId,
                    Model = prod.Model,
                    Manufacturer = prod.Manufacturer,
                    Quantity = op.Quantity,
                    InventoryQuantity = prod.InventoryQuantity,
                    UnitPrice = op.UnitPrice,
                    Color = op.Color,
                    Size = op.Size,
                    Subtotal = order.Subtotal,
                    Shipping = order.ShippingCost,
                    Tax = order.Tax,
                    Total = order.Total
                };

            return (prods);
            //var orderSummary = (from op in _context.OrderProduct
            //                     join p in _context.Product on op.ProductId equals p.Id
            //                     where op.OrderId == id
            //                     select new OrderSummary()
            //                     {
            //                         Tax = order.Tax,
            //                         SourceCode = order.SourceCode,
            //                         ShippingAddressId = order.ShippingAddressId,
            //                         ShippingCost = order.ShippingCost,
            //                         Subtotal = order.Subtotal,
            //                         Total = order.Total,
            //                     }).ToList();

            //if (order == null)
            //{
            //    return NotFound();
            //}

            //// Retrieve all products for the order
            //var orderProducts = _context.OrderProduct.Where(p => p.OrderId == id);

            //// Map results to new object
            //var orderSummary = new OrderSummary()
            //{
            //    TimeStamp = order.TimeStamp,
            //    Tax = order.Tax,
            //    SourceCode = order.SourceCode,
            //    ShippingAddressId = order.ShippingAddressId,
            //    ShippingCost = order.ShippingCost,
            //    Subtotal = order.Subtotal,
            //    Total = order.Total,

            //}

            //return Ok(order);
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
        [EnableCors("AllowMyOrigin")]
        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] CartProduct productInCart)
        {
            var shoppingCart = await _context.ShoppingCart.FirstOrDefaultAsync(c => c.Id.Equals(productInCart.CartId)) as ShoppingCart;
            if (shoppingCart.OrderPlaced == true)
                return BadRequest();

            // Check cart has not already placed order
            if (!(bool)shoppingCart.OrderPlaced)
            {
                // Create a new order
                var order = new Order();
                order.TimeStamp = DateTime.Now;
                order.Tax = productInCart.cartTotal != string.Empty ? decimal.Parse(productInCart.cartTotal) * 0.06m :  0.0m;
                order.SourceCode = productInCart.SourceCode != string.Empty ? productInCart.SourceCode : string.Empty;
                order.ShippingAddressId = -1;
                order.UserId = _context.Session.Where(s => s.SessionId.Equals(productInCart.sessionId))
                                       .Select(s => s.UserId)
                                       .FirstOrDefault();
                order.ShippingCost = _context.ShippingMethod
                                             .Where(x => x.Description.Contains("FedEx"))
                                             .Select(x => x.Rate)
                                             .FirstOrDefault();
                order.ShoppingCartId = productInCart.CartId;

                decimal.TryParse(productInCart.cartTotal, out decimal subtotal);
                order.Subtotal = subtotal;
                order.Total = order.Subtotal + order.Tax + order.ShippingCost;
                
                _context.Order.Add(order);
                await _context.SaveChangesAsync();

                // Add Cart products to a new OrderProduct and save
                foreach (var product in productInCart.cartProducts)
                {
                    var orderProduct = new OrderProduct()
                    {
                        ProductId = _context.ShoppingCartProduct
                                            .Where(x => x.Id == product.Id)
                                            .Select(x => x.ProductId)
                                            .FirstOrDefault(),

                        Quantity = product.Quantity,
                        Size = product.Size,
                        Color = product.Color,
                        UnitPrice = product.UnitPrice,
                        OrderId = order.Id,
                    };

                    _context.OrderProduct.Add(orderProduct);
                    await _context.SaveChangesAsync();
                }

                // Update shoppingCart to show order has been placed
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

                return Ok(order.Id);
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


        public class OrderSummary
        {
            public int Id { get; set; }
            public DateTime? TimeStamp { get; set; }
            public decimal Tax { get; set; }
            public string SourceCode { get; set; }
            public int ShippingAddressId { get; set; }
            public decimal ShippingCost { get; set; }
            public decimal Subtotal { get; set; }
            public decimal Total { get; set; }
            public List<Product> products { get; set; }
            public int? Quantity { get; set; }
            public string Size { get; set; }
            public string Color { get; set; }
            public decimal? UnitPrice { get; set; }
        }


        public class OrderItems
        {
            public int Id { get; set; }
            public int CartId { get; set; }
            public string Manufacturer { get; set; }
            public string Model { get; set; }
            public string Size { get; set; }
            public string Color { get; set; }
            public decimal Price { get; set; }
            public int? InventoryQuantity { get; set; }
            public int CartQuantity { get; set; }
            public int? Quantity { get; set; }
            public decimal? UnitPrice { get; set; }
            public decimal Subtotal { get; set; }
            public decimal Tax { get; set; }
            public decimal Shipping { get; set; }
            public decimal Total { get; set; }
        }
    }
}