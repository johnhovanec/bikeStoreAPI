using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bikestoreAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;

namespace bikestoreAPI.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;

            if (_context.Product.Count() == 0)
            {
                _context.Product.Add(new Product {
                    Manufacturer = "Trek",
                    Model = "8400",
                    Type = "Mountain",
                    Price = 989.99m,
                    Color ="silver",
                    Size = "XL",
                    InventoryQuantity = 12,
                    ImagePath = ""
                });

                _context.Product.Add(new Product
                {
                    Manufacturer = "Giant",
                    Model = "ARX",
                    Type = "Mountain",
                    Price = 459.99m,
                    Color = "black",
                    Size = "S",
                    InventoryQuantity = 6,
                    ImagePath = ""
                });
                _context.SaveChanges();
            }

        }

        // GET: api/Products
        [HttpGet]
        public IEnumerable<Product> GetProduct([FromQuery] string searchTerm)
        {
            var products = from p in _context.Product
                           select p;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var typeMatches = products.Where(p => p.Type.Contains(searchTerm));
                if (typeMatches.Count() > 0)
                    return typeMatches;

                var manufacturerMatches = products.Where(p => p.Manufacturer.Contains(searchTerm));
                if (manufacturerMatches.Count() > 0)
                    return manufacturerMatches;

                var modelMatches = products.Where(p => p.Model.Contains(searchTerm));
                if (modelMatches.Count() > 0)
                    return modelMatches;
            }
            return products;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        //PATCH: api/Products/1
        [EnableCors("AllowMyOrigin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProduct([FromRoute] int id, [FromBody]JsonPatchDocument<Product> request)
        {
            var product = _context.Product.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            else
            {
                product.Rating = request.Operations.FirstOrDefault().value.ToString();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
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

        // POST: api/Products
        [EnableCors("AllowMyOrigin")]
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}


public class RatingPatchRequest
{
    public string Op { get; set; }
    public string Path { get; set; }
    public string Value { get; set; }
}