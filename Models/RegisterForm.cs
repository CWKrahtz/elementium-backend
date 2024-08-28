public class RegisterForm()
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
    public required string Password { get; set; }
    public string? Created_at { get; set; }
}