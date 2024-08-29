using System;

namespace elementium_backend.Models;

public interface ExchangeRequest
{
    public int UserId { get; set; }
    public decimal CurrencyAmount { get; set; }
    public decimal Cost { get; set; }
}