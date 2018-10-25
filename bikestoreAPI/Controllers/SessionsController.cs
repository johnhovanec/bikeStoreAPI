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
    [Route("api/Sessions")]
    public class SessionsController : Controller
    {
        private readonly StoreContext _context;

        public SessionsController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/Sessions
        [HttpGet]
        public IEnumerable<Session> GetSession()
        {
            return _context.Session;
        }

        // GET: api/Sessions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSession([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var session = await _context.Session.SingleOrDefaultAsync(m => m.Id == id);

            if (session == null)
            {
                return NotFound();
            }

            return Ok(session);
        }

        // PUT: api/Sessions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSession([FromRoute] int id, [FromBody] Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != session.Id)
            {
                return BadRequest();
            }

            _context.Entry(session).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(id))
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

        // POST: api/Sessions
        [EnableCors("AllowMyOrigin")]
        [HttpPost]
        public async Task<IActionResult> PostSession([FromBody] Login login)
        {
            var session = new Session();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = from u in _context.User
                           select u;
            if (!string.IsNullOrEmpty(login.Username))
            {
                var user = users.Where(u => u.Username.Equals(login.Username)).FirstOrDefault();
                if (user == null)
                    return NotFound();
                //string userId = Convert.ToString(user.Id);
                if (user.Password.Equals(login.Password))
                {
                    // Login success
                    session.UserSessionType = user.Type;
                    session.SessionStart = DateTime.Parse(login.Timestamp);
                    session.SessionExpires = DateTime.Parse(login.Timestamp).AddDays(30);
                    session.SessionId = login.SessionId;
<<<<<<< HEAD
                    //session.UserId = user.Id;
=======
                    session.UserId = user.Id;
>>>>>>> bb95a6d152dc019ffdf5423363b3c977553ef618
                    _context.Session.Add(session);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetSession", new { id = session.Id }, session);
                }
            }

            // Login failure
            return NotFound();
        }

        // DELETE: api/Sessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var session = await _context.Session.SingleOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            _context.Session.Remove(session);
            await _context.SaveChangesAsync();

            return Ok(session);
        }

        private bool SessionExists(int id)
        {
            return _context.Session.Any(e => e.Id == id);
        }
    }

    // Model used for JSON login object
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string SessionId { get; set; }
        public string Timestamp { get; set; }
        //public int UserId { get; set; }
    }

}
