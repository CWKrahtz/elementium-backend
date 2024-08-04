using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elementium_backend;

public class Users
{
    //Primary Key
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId {get; set;}
    public string? Username {get; set;}
    public string? Email {get; set;}
    public string? Role {get; set;}
    public string? Created_at {get; set;}
}
