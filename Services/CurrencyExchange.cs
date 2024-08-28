using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using elementium_backend.Models;

namespace elementium_backend.Services
{
    public class CurrencyExchangeService
    {
        private readonly AppDbContext _context;

        public CurrencyExchangeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FeedbackResponse> ExchangeCurrency(ExchangeRequest request)
        {
            // Retrieve the user's account
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == request.UserId);

            if (account == null)
            {
                return new FeedbackResponse
                {
                    Type = StatusCodes.Status404NotFound,
                    Status = "Error",
                    Message = "User account not found.",
                    Body = null
                };
            }

            // Check if the account has enough balance
            if (account.Balance < request.Cost)
            {
                return new FeedbackResponse
                {
                    Type = StatusCodes.Status400BadRequest,
                    Status = "Error",
                    Message = "Insufficient funds in the account.",
                    Body = null
                };
            }

            // Deduct the cost from the account balance
            account.Balance -= (int)request.Cost;

            // Create a new transaction
            var transaction = new Transaction
            {
                // ToAccount = request.UserId,
                // Amount = request.CurrencyAmount,
                // TransactionType = "Purchase",
                // Date = DateTime.UtcNow
                // Add any additional fields as necessary
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            // Return a success response
            return new FeedbackResponse
            {
                Type = StatusCodes.Status200OK,
                Status = "Success",
                Message = "Currency exchange successful.",
                Body = transaction
            };
        }
    }
}
