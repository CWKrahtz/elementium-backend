using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace YourNamespace.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Send2FaCodeAsync(string toEmail, string code)
        {
            var apiKey = _configuration["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("21100419@virtualwindow.co.za", "Elementium");
            var subject = "Your 2FA Code";
            var to = new EmailAddress(toEmail);
            var plainTextContent = $"Your 2FA code is {code}";
            var htmlContent = $"<strong>Your 2FA code is {code}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
        }
    }
}
