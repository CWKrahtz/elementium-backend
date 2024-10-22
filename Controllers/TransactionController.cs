using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using elementium_backend;
using elementium_backend.Models;
using elementium_backend.Services;
using Microsoft.AspNetCore.Cors;  // Ensure this using directive is present

namespace elementium_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;
        // private readonly CurrencyExchangeService _currencyExchangeService;

        // Inject both AppDbContext and CurrencyExchangeService via the constructor
        public TransactionController(AppDbContext context) //, CurrencyExchangeService currencyExchangeService
        {
            _context = context;
            // _currencyExchangeService = currencyExchangeService;
        }

        // GET: api/Transaction
        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _context.Transactions
                                             .Include(t => t.FromAccount)
                                             .Include(t => t.ToAccount)
                                             .ToListAsync();

            if (!transactions.Any())
            {
                return NotFound();
            }

            return Ok(transactions);
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            // var transaction = await _context.Transactions.FindAsync(id);
            var transaction = await _context.Transactions
                                    .Include(t => t.FromAccount)
                                    .Include(t => t.ToAccount)
                                    .Where(t => t.TransactionId == id)
                                    .FirstOrDefaultAsync();

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        // PUT: api/Transaction/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.TransactionId)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transaction
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = transaction.TransactionId }, transaction);
        }

        // DELETE: api/Transaction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }

        // POST: api/Transaction/exchange
        // [HttpPost("exchange")]
        // public async Task<ActionResult<FeedbackResponse>> ExchangeCurrency([FromBody] ExchangeRequest request)
        // {
        //     var feedback = await _currencyExchangeService.ExchangeCurrency(request);
        //     if (feedback.Type == StatusCodes.Status200OK)
        //     {
        //         return Ok(feedback);
        //     }
        //     else
        //     {
        //         // Ensure feedback.Type is a valid HTTP status code
        //         return StatusCode(feedback.Type, feedback);
        //     }
        // }

    }
}
