using System.ComponentModel.DataAnnotations;

public class LoginForm
{
    [Key]
    public string? Email { get; set; }
    public string? Passowrd { get; set; }
}
