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
    public DateTime? Timestamp {get; set;}//Date and Time of Transaction

    // Foreign Keys
    //Link the Fk of this table with PK  -- Do not understand how it works
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }

    // Navigation properties - net vir hoe data return word
    [ForeignKey("FromAccountId")]
    public Account? FromAccount { get; set; }

    [ForeignKey("ToAccountId")]
    public Account? ToAccount { get; set; }
}