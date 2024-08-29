using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using elementium_backend;

namespace elementium_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSecurityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserSecurityController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserSecurity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSecurity>>> Getuser_security()
        {
            return await _context.user_security.ToListAsync();
        }

        // GET: api/UserSecurity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSecurity>> GetUserSecurity(int id)
        {
            // var userSecurity = await _context.user_security.FindAsync(id);
            UserSecurity userSecurity = await _context.user_security
            .Include(us => us.Users)
            .SingleOrDefaultAsync(us => us.SecurityId == id);

            if (userSecurity == null)
            {
                return NotFound();
            }

            return userSecurity;
        }

        // PUT: api/UserSecurity/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserSecurity(int id, UserSecurity userSecurity)
        {
            if (id != userSecurity.SecurityId)
            {
                return BadRequest();
            }

            _context.Entry(userSecurity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSecurityExists(id))
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

        // POST: api/UserSecurity
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserSecurity>> PostUserSecurity(UserSecurity userSecurity)
        {
            _context.user_security.Add(userSecurity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserSecurity", new { id = userSecurity.SecurityId }, userSecurity);
        }

        // DELETE: api/UserSecurity/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSecurity(int id)
        {
            var userSecurity = await _context.user_security.FindAsync(id);
            if (userSecurity == null)
            {
                return NotFound();
            }

            _context.user_security.Remove(userSecurity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserSecurityExists(int id)
        {
            return _context.user_security.Any(e => e.SecurityId == id);
        }
    }
}
