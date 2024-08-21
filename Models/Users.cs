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


    // Original declaration of created at. This version is stored as a string but is of data type DateTime
    // public DateOnly? Created_at {get; set;}//Date/Time??

    // Just using this string declaration for now since it stores as a string and breaks the server when it doesnt get a datetime data object from the fetch.
    public string? Created_at {get; set;}

    public ICollection<AuthenticationLog>? AuthenticationLog {get; set;}
    public UserSecurity? UserSecurity {get; set;}
    public Account? Account {get; set;}
}