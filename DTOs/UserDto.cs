using System;

namespace elementium_backend.DTOs;

public class UserDto
{
    public int UserId {get; set;}
    public string Username {get; set;}
    public string Email {get; set;}
    public string? Role {get; set;}
    public string? Created_at {get; set;}

    // public ICollection<AuthenticationLog>? AuthenticationLog {get; set;}
    
    // public UserSecurity? UserSecurity {get; set;}
    public Account? Account {get; set;}
}
