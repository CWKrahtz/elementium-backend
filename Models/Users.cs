﻿using System.ComponentModel.DataAnnotations;
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
    public DateOnly? Created_at {get; set;}//Date/Time??

    public ICollection<AuthenticationLog>? AuthenticationLog {get; set;}
    public UserSecurity? UserSecurity {get; set;}
    public Account? Account {get; set;}
}