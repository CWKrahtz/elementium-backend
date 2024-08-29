using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using YourNamespace.Services;

namespace elementium_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OTPController : ControllerBase
    {
        private readonly EmailService _emailService;
        private static string _generated2FaCode;

        public OTPController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-code")]
        public async Task<IActionResult> Send2FaCode([FromBody] SendOTP request)
        {
            if (string.IsNullOrEmpty(request?.Email))
            {
                return BadRequest("Email is required.");
            }

            // Generate a 2FA code
            _generated2FaCode = Generate2FaCode();

            // Send the code via email
            await _emailService.Send2FaCodeAsync(request.Email, _generated2FaCode);

            return Ok($"2FA code sent successfully to {request.Email}. Code: {_generated2FaCode}");
        }

/// <summary>
/// Verifies OTP
/// </summary>
/// <param name="code"></param>
/// <returns></returns>
        [HttpPost("verify-code")]
        public IActionResult Verify2FaCode([FromBody] string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest("Code is required.");

            // Validate the 2FA code
            if (Validate2FaCode(code, _generated2FaCode))
            {
                return Ok("2FA code is valid.");
            }

            return Unauthorized("Invalid 2FA code.");
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
