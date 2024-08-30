using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using elementium_backend.Services;

namespace elementium_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OTPController : ControllerBase
    {
        private readonly IOtpService _otpService;
        private static string _generated2FaCode = "";

        public OTPController(IOtpService otpService)
        {
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
