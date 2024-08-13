﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elementium_backend;

public class Account
{
    //Primary Key
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AccountId { get; set; }
    public int Balance { get; set; }
    public bool Active { get; set; }

    //Foreign Key
    public int UserId { get; set; }
    public int AccountStatusId { get; set; }

    // Navigation property
    public Status? Status { get; set; }
    public Users? User { get; set; }

    // Separate navigation properties for FromTransactions and ToTransactions
    public ICollection<Transaction>? FromTransactions { get; set; }
    public ICollection<Transaction>? ToTransactions { get; set; }
}