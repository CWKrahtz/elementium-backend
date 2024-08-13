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

    // Foreign Keys
    //Link the Fk of this table with PK  -- Do not understand how it works
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }

    // Navigation properties
    [ForeignKey("FromAccountId")]
    public Account? FromAccount { get; set; }

    [ForeignKey("ToAccountId")]
    public Account? ToAccount { get; set; }
}