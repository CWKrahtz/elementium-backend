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
    public class AuthenticationLogController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthenticationLogController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AuthenticationLog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthenticationLog>>> GetAuthenticationLogs()
        {
            return await _context.AuthenticationLogs.ToListAsync();
        }

        // GET: api/AuthenticationLog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthenticationLog>> GetAuthenticationLog(int id)
        {
            // var authenticationLog = await _context.AuthenticationLogs.FindAsync(id);
            AuthenticationLog authenticationLog = await _context.AuthenticationLogs
                                                                .Include(al => al.User)
                                                                .SingleOrDefaultAsync(al => al.LogId == id);


            if (authenticationLog == null)
            {
                return NotFound();
            }

            return authenticationLog;
        }

        // PUT: api/AuthenticationLog/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthenticationLog(int id, AuthenticationLog authenticationLog)
        {
            if (id != authenticationLog.LogId)
            {
                return BadRequest();
            }

            _context.Entry(authenticationLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthenticationLogExists(id))
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

        // POST: api/AuthenticationLog
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthenticationLog>> PostAuthenticationLog(AuthenticationLog authenticationLog)
        {
            _context.AuthenticationLogs.Add(authenticationLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthenticationLog", new { id = authenticationLog.LogId }, authenticationLog);
        }

        // DELETE: api/AuthenticationLog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthenticationLog(int id)
        {
            var authenticationLog = await _context.AuthenticationLogs.FindAsync(id);
            if (authenticationLog == null)
            {
                return NotFound();
            }

            _context.AuthenticationLogs.Remove(authenticationLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthenticationLogExists(int id)
        {
            return _context.AuthenticationLogs.Any(e => e.LogId == id);
        }
    }
}
