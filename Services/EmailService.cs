using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

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
            var apiKey = _configuration["Mailgun:ApiKey"];
            var domain = _configuration["Mailgun:Domain"];
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"api:{apiKey}")));

            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("from", "Elementium Team <21100419@virtualwindow.co.za>"),
                new KeyValuePair<string, string>("to", toEmail),
                new KeyValuePair<string, string>("subject", "Verfication Code"),
                new KeyValuePair<string, string>("text", $"Your 2FA code is {code}"),
                new KeyValuePair<string, string>("html", $"<strong>Your 2FA code is {code}</strong>")
            });

            var response = await client.PostAsync($"https://api.mailgun.net/v3/{domain}/messages", requestContent);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                // Log the response body for debugging
                throw new Exception($"Failed to send email via Mailgun. Status: {response.StatusCode}, Error: {responseBody}");
            }
        }
    }
}
