using elementium_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using elementium_backend.Services;
using System;

namespace elementium_backend.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IOtpService _otpService;
        private readonly IPasswordService _passwordService;

        public RegisterController(AppDbContext context, IOtpService otpService, IPasswordService passwordService)
        {
            _context = context;
            _otpService = otpService;
            _passwordService = passwordService;
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser(RegisterForm form)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Check if the user with the given email already exists
                var existingUser = await _context.users
                    .FirstOrDefaultAsync(u => u.Email == form.Email);

                if (existingUser != null)
                {
                    var feedback = new FeedbackResponse
                    {
                        Type = StatusCodes.Status400BadRequest,
                        Status = "Error",
                        Message = "User already exists."
                    };
                    return BadRequest(feedback);
                }

                // Create new user
                var newUser = new Users
                {
                    Email = form.Email,
                    Username = form.Username,
                    Role = string.IsNullOrEmpty(form.Role) || form.Role != "admin" ? "user" : form.Role,
                    Created_at = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
                };

                _context.users.Add(newUser);
                await _context.SaveChangesAsync();

                // Hash the password and create a new user_security record
                var hashedPassword = _passwordService.HashPassword(form.Password);
                var userSecurity = new UserSecurity
                {
                    UserId = newUser.UserId,
                    Password_hash = hashedPassword
                };

                _context.user_security.Add(userSecurity);
                await _context.SaveChangesAsync();

                // Ensure the AccountStatusId is set to a valid value
                var defaultAccountStatusId = _context.status.FirstOrDefault(s => s.Status_name == "Reactive")?.StatusId;
                if (defaultAccountStatusId == null)
                {
                    throw new Exception("Default account status not found.");
                }

                // Create account record for the user
                var account = new Account
                {
                    UserId = newUser.UserId,
                    Active = true,
                    Balance_h2 = 1000,
                    Balance_li = 0,
                    Balance_pd = 0,
                    Balance_xe = 0,
                    AccountStatusId = defaultAccountStatusId.Value
                };

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();

                // Optionally, create an entry in AuthenticationLogs or any other linked tables
                var authLog = new AuthenticationLog
                {
                    UserId = newUser.UserId,
                    // other properties with default values
                };

                _context.AuthenticationLogs.Add(authLog);
                await _context.SaveChangesAsync();

                // Send the 2FA code via OTPController after successful registration
                await _otpService.Send2FaCodeAsync(newUser.Email, newUser.UserId);

                // Commit the transaction
                await transaction.CommitAsync();

                var successFeedback = new FeedbackResponse
                {
                    Type = StatusCodes.Status201Created,
                    Status = "Success",
                    Message = "User registration successful. 2FA sent to your email",
                    Body = newUser
                };
                return CreatedAtAction(nameof(RegisterUser), new { id = newUser.UserId }, successFeedback);
            }
            catch (Exception ex)
            {
                // Rollback the transaction if any error occurs
                await transaction.RollbackAsync();

                // Log the error (you can implement a logging mechanism here)
                Console.WriteLine(ex.Message);

                var feedback = new FeedbackResponse
                {
                    Type = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = ex.Message,
                    Body = ex.ToString()
                };
                return StatusCode(StatusCodes.Status500InternalServerError, feedback);
            }
        }
    }
}
