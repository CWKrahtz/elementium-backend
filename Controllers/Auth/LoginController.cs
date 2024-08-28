using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace elementium_backend.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Account>> LoginUser(LoginForm form)
        {
            _context.Login.Add(form);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = form.Email }, form);
        }
    }
}
