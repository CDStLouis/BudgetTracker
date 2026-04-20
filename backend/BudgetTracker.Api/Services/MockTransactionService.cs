using BudgetTracker.Api.Models;

namespace BudgetTracker.Api.Services
{
    public class MockTransactionService : ITransactionService
    {
        public Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            var transactions = new List<Transaction>
            {
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddDays(-1), Description = "Aldi", Amount = -45.32m, Category = "Groceries", AccountName = "Santander" },
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddDays(-2), Description = "Netflix", Amount = -15.99m, Category = "Entertainment", AccountName = "Monzo" },
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddDays(-3), Description = "Salary", Amount = 3500.00m, Category = "Income", AccountName = "Santander" },
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddDays(-4), Description = "Uber Eats", Amount = -22.50m, Category = "Eating Out", AccountName = "Monzo" },
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddDays(-5), Description = "Trainline", Amount = -4.80m, Category = "Transport", AccountName = "Santander" },
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddDays(-6), Description = "Gym Membership", Amount = -40.00m, Category = "Health", AccountName = "Monzo" },
            };

            return Task.FromResult<IEnumerable<Transaction>>(transactions);
        }
    }
}