using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elementium_backend;

public class Users
{
    //Primary Key
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId {get; set;}
    public required string Username {get; set;}
    public required string Email {get; set;}
    public string? Role {get; set;}
    public string? Created_at {get; set;}

    public ICollection<UserSecurity>? AuthenticationLog {get; set;}
    public UserSecurity? UserSecurity {get; set;}
    
}
