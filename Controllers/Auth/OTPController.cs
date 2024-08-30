using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using elementium_backend.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using elementium_backend.Models;
using Microsoft.EntityFrameworkCore;


namespace elementium_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OTPController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IOtpService _otpService;
        private static string _generated2FaCode = "";

        public OTPController(AppDbContext context, IOtpService otpService)
        {
            _context = context;
            _otpService = otpService;
        }

        /// <summary>
        /// Generates 2FA code and sends it to the provided email.
        /// </summary>
        [HttpPost("send-code")]
        public async Task<IActionResult> Send2FaCode([FromBody] SendOTP request)
        {
            if (string.IsNullOrEmpty(request?.Email))
            {
                return BadRequest("Email is required.");
            }

            // Generate a 2FA code
            _generated2FaCode = Generate2FaCode();

            var useremail = request.Email;
            var userId = request.UserId;

            // Send the code via the OTP service
            await _otpService.Send2FaCodeAsync(useremail, userId);

            return Ok($"2FA code sent successfully to {request.Email}. Code: {_generated2FaCode}");
        }

        /// <summary>
        /// Verifies the provided 2FA code.
        /// </summary>
        [HttpPost("verify-code")]
        public async Task<IActionResult> Verify2FaCode([FromBody] TwoFactorVerificationRequest request)
        {
            if (string.IsNullOrEmpty(request.Code))
            {
                var authDenyFeedback = new FeedbackResponse
                {
                    Type = StatusCodes.Status400BadRequest,
                    Status = "Error",
                    Message = "Code is required."
                };
                return BadRequest(authDenyFeedback);
            }

            var user = await _context.users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                var userNotFoundFeedback = new FeedbackResponse
                {
                    Type = StatusCodes.Status404NotFound,
                    Status = "Error",
                    Message = "User not found."
                };
                return NotFound(userNotFoundFeedback);
            }

            var user_security = await _context.user_security
            .FirstOrDefaultAsync(us => us.UserId == user.UserId);
            if (user_security == null)
            {
                var userNotFoundFeedback = new FeedbackResponse
                {
                    Type = StatusCodes.Status404NotFound,
                    Status = "Error",
                    Message = "User's security not found."
                };
                return NotFound(userNotFoundFeedback);
            }

            // Assuming Validate2FaCode is a method that validates the code
            if (user_security.Latest_otp_secret == request.Code)
            {
                // Update the Latest_otp_secret field
                user_security.IsOtpVerified = true;

                // Save the changes
                _context.user_security.Update(user_security);
                await _context.SaveChangesAsync();

                var successFeedback = new FeedbackResponse
                {
                    Type = StatusCodes.Status200OK,
                    Status = "Success",
                    Message = "Code verification successful."
                };
                return Ok(successFeedback);
            }

            var unauthFeedback = new FeedbackResponse
            {
                Type = StatusCodes.Status403Forbidden,
                Status = "Error",
                Message = "Access denied because is wrong. Try again.",
                Body = user_security.Latest_otp_secret
            };
            return Unauthorized(unauthFeedback);
        }

        // Model for the request payload
        public class TwoFactorVerificationRequest
        {
            public string Email { get; set; }
            public string Code { get; set; }
        }

        private string Generate2FaCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private bool Validate2FaCode(string userCode, string actualCode)
        {
            return userCode == actualCode;
        }
    }
}
