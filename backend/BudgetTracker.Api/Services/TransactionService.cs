using BudgetTracker.Api.Data;
using BudgetTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BudgetTrackerContext _context;

        public TransactionService(BudgetTrackerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            return await _context.Transactions.ToListAsync();
        }
    }
}