using System;

namespace elementium_backend.DTOs;

public class UserDto
{
    public string Username {get; set;}
    public string Email {get; set;}
    public string? Role {get; set;}
    public string? Created_at {get; set;}
    public string? Balance_h2 {get; set;}
    public string? Balance_li {get; set;}
    public string? Balance_pd {get; set;}
    public string? Balance_xe {get; set;}
    public bool Active {get; set;}
}
