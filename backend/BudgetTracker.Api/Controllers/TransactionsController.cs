using BudgetTracker.Api.Contracts;
using BudgetTracker.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _transactionService.GetTransactionsAsync();
            var response = transactions
                .Select(transaction =>
                {
                    var date = transaction.Date;
                    return new TransactionDto
                    {
                        Id = transaction.Id,
                        DateUtc = DateTime.SpecifyKind(date, DateTimeKind.Utc),
                        Description = transaction.Description,
                        SignedAmount = transaction.Amount,
                        AbsoluteAmount = Math.Abs(transaction.Amount),
                        Type = transaction.Amount < 0 ? "expense" : "income",
                        Category = transaction.Category,
                        AccountName = transaction.AccountName,
                        MonthKey = date.ToString("yyyy-MM"),
                        DateKey = date.ToString("yyyy-MM-dd")
                    };
                })
                .OrderByDescending(transaction => transaction.DateUtc);

            return Ok(response);
        }

    }
}
