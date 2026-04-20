using BudgetTracker.Api.Models;

namespace BudgetTracker.Api.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync();
    }
}
