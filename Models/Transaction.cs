using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elementium_backend;

public class Transaction
{
    //Primary Key
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionId {get; set;}
    public string? TransactionType {get; set;}
    public int Amount {get; set;}
    public string? Timestamp {get; set;}
    //Foreign Key
    public string? FromAccountId {get; set;}
    public string? ToAccountId {get; set;}
}
