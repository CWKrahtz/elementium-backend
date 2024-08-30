using elementium_backend;
using elementium_backend.Services;
using Microsoft.EntityFrameworkCore;

public interface IOtpService
{
    Task Send2FaCodeAsync(string email, int userId);
}

public class OtpService : IOtpService
{
    private readonly AppDbContext _context;
    private readonly EmailService _emailService;

    public OtpService(EmailService emailService, AppDbContext context)
    {
        _emailService = emailService;
        _context = context;
    }
    public async Task Send2FaCodeAsync(string email, int userId)
    {
        var generated2FaCode = Generate2FaCode();

        // Update the UserSecurity table with the generated OTP code
        try
        {
            var userSecurity = await _context.user_security.FirstOrDefaultAsync(us => us.UserId == userId);
            if (userSecurity == null)
            {
                // Log or handle the case where the user is not found
                Console.WriteLine($"UserSecurity record not found for userId: {userId}");
                // Optionally, you could throw an exception or return an error code here
                return;
            }

            // Update the Latest_otp_secret field
            userSecurity.Latest_otp_secret = generated2FaCode;

            // Save the changes
            _context.user_security.Update(userSecurity);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException dbEx)
        {
            // Handle database update exceptions
            Console.WriteLine($"Database update failed for userId: {userId}, Error: {dbEx.Message}");
            // Optionally, log more details, rethrow the exception, or return an error code
            return;
        }
        catch (Exception ex)
        {
            // Handle any other exceptions
            Console.WriteLine($"An error occurred while updating the OTP secret for userId: {userId}, Error: {ex.Message}");
            // Optionally, log more details, rethrow the exception, or return an error code
            return;
        }

        // Send the code via email
        await _emailService.Send2FaCodeAsync(email, generated2FaCode);
    }

    private string Generate2FaCode()
    {
        Random random = new Random();
        return random.Next(100000, 999999).ToString();
    }
}
