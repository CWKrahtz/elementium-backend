using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elementium_backend;

public class AuthenticationLog
{
    //Primary Key
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LogId{get; set;}
    public int LoginTime{get; set;}
    public int LogoutTime{get; set;}
    public int IpAddress{get; set;}
    public int DeviceInfo{get; set;}

    //Foreign Key
    public int UserId {get; set;}
}
