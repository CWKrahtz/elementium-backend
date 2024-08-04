using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elementium_backend;

public class Status
{
    //Primary Key
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StatusId {get; set;}
    public string? Status_name {get; set;}
    public int Total_amount_criteria {get; set;}
    public string? Transactions_criteria {get; set;}
    public float Annual_interest_rate {get; set;}
    public float Transaction_fee {get; set;}
}
