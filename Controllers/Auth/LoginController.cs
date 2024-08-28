using elementium_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
            var allUsers = await _context.users.ToListAsync();
            // Validate the user's credentials by querying the Users table
            var user = await _context.users
                // .FirstOrDefaultAsync(u => u.Email == form.Email && u.UserSecurity.Password_hash == form.Passowrd);
                .FirstOrDefaultAsync(u => u.Email == form.Email);

            if (user == null)
            {
                // If no matching user is found, return an Unauthorized result
                // Return Unauthorized feedback

                var feedback = new FeedbackResponse
                {
                    Type = StatusCodes.Status401Unauthorized,
                    Status = "Error",
                    Message = "Invalid email or password.",
                    Body = new
                    {
                        InputEmail = form.Email,
                        AllUsers = allUsers
                    }
                };
                return Unauthorized(feedback);
            }

            // Retrieve the associated account information
            var account = await _context.users
                .FirstOrDefaultAsync(a => a.UserId == user.UserId);

            if (account == null)
            {
                var feedback = new FeedbackResponse
                {
                    Type = StatusCodes.Status404NotFound,
                    Status = "Error",
                    Message = "Account not found for this user",
                    Body = new
                    {
                        UserId = user.UserId,
                        AllUsers = allUsers
                    }
                };
                return NotFound(feedback);
            }

            // Return the account information on successful login
            var successFeedback = new FeedbackResponse
            {
                Type = StatusCodes.Status200OK,
                Status = "Success",
                Message = "Login successful",
                Body = account
            };
            return Ok(successFeedback);
        }
    }
}
