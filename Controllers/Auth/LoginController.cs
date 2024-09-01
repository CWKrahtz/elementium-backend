using elementium_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using elementium_backend.Services;

namespace elementium_backend.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IOtpService _otpService;
        private readonly IPasswordService _passwordService;


        public LoginController(AppDbContext context, IOtpService otpService, IPasswordService passwordService)
        {
            _context = context;
            _otpService = otpService;
            _passwordService = passwordService;
        }

        [HttpPost]
        public async Task<ActionResult<Account>> LoginUser(LoginForm form)
        {
            var allUsers = await _context.users.ToListAsync();

            // Validate the user's credentials by querying the Users table
            var user = await _context.users
                .FirstOrDefaultAsync(u => u.Email == form.Email);

            if (user == null)
            {
                // If no matching user is found, return an Unauthorized result
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
                .Include(u => u.Account)
                .Include(u => u.AuthenticationLog)
                .Include(u => u.UserSecurity)
                .Where(a => a.UserId == user.UserId)
                .ToListAsync();

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

            var user_security = await _context.user_security
                        .FirstOrDefaultAsync(us => us.UserId == user.UserId);

            // Check if password matches
            if (!_passwordService.VerifyPassword(user_security.Password_hash, form.Password))
            {
                var feedback = new FeedbackResponse
                {
                    Type = StatusCodes.Status400BadRequest,
                    Status = "Error",
                    Message = "Incorrect email or password.",
                    Body = null
                };
                return BadRequest(feedback);
            }

            // Update the Latest_otp_secret field
            user_security.IsOtpVerified = false;

            // Save the changes
            _context.user_security.Update(user_security);
            await _context.SaveChangesAsync();

            // Send the 2FA code via OTPController after successful login
            var useremail = form.Email;
            var userId = user.UserId;

            await _otpService.Send2FaCodeAsync(useremail, userId);
            var successFeedback = new FeedbackResponse
            {
                Type = StatusCodes.Status200OK,
                Status = "Success",
                Message = "Login successful. 2FA code has been sent to your email.",
                Body = account
            };
            return Ok(successFeedback);
        }

    }
}
