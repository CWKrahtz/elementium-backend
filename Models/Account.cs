﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elementium_backend;

public class Account
{
    //Primary Key
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AccountId { get; set; }
    public int Balance_h2 { get; set; }
    public int Balance_li { get; set; }
    public int Balance_pd { get; set; }
    public int Balance_xe { get; set; }
    public bool Active { get; set; }

    //Foreign Key
    public int UserId { get; set; }
    public int AccountStatusId { get; set; }

#region  NavProperty
    // Navigation property
    public Status? Status { get; set; }
    public Users? User { get; set; }

#endregion

    // Separate navigation properties for FromTransactions and ToTransactions
    public ICollection<Transaction>? FromTransactions { get; set; }
    public ICollection<Transaction>? ToTransactions { get; set; }
}