using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elementium_backend;

public class AuthenticationLog
{
    //Primary Key
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LogId{get; set;}
    public DateTime? LoginTime{get; set;}//User login date and time
    public DateTime? LogoutTime{get; set;}//User logout date and time
    public int IpAddress{get; set;}
    public int DeviceInfo{get; set;}

    //Link the Fk of this table with PK 
    //-------(using it if field names are different)
    [ForeignKey("User")]
    public int UserId { get; set; }
    //navigation
    public Users? User {get; set;}
}
