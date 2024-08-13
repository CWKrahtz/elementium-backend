using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elementium_backend;

public class UserSecurity
{
    //Primary Key
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SecurityId {get; set;}
    public string? Password_hash {get; set;}
    public string? Latest_otp_secret {get; set;}
    public string? Uploaded_at {get; set;}

    //Foreign Key
    public int UserId {get; set;}

    [ForeignKey("UserId")]
    public Users? Users { get; set; }
}
