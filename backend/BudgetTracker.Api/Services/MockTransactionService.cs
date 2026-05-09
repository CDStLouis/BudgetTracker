using BudgetTracker.Api.Models;

namespace BudgetTracker.Api.Services
{
    public class MockTransactionService : ITransactionService
    {
        public Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            var transactions = new List<Transaction>
            {
                // Month -1
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddMonths(-1).AddDays(-2), Description = "Aldi", Amount = -45.32m, Category = "Groceries", AccountName = "Santander" },
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddMonths(-1).AddDays(-7), Description = "Rent", Amount = -950.00m, Category = "Housing", AccountName = "Santander" },
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddMonths(-1).AddDays(-14), Description = "Cafe Latte", Amount = -5.60m, Category = "Eating Out", AccountName = "Monzo" },

                // Month -2
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddMonths(-2).AddDays(-6), Description = "Netflix", Amount = -15.99m, Category = "Entertainment", AccountName = "Monzo" },                
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddMonths(-2).AddDays(-18), Description = "Trainline", Amount = -4.80m, Category = "Transport", AccountName = "Santander" },

                // Month -3
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddMonths(-3).AddDays(-10), Description = "Salary", Amount = 3500.00m, Category = "Income", AccountName = "Santander" },
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddMonths(-3).AddDays(-13), Description = "Gym Membership", Amount = -40.00m, Category = "Health", AccountName = "Monzo" },
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddMonths(-3).AddDays(-20), Description = "Amazon", Amount = -24.50m, Category = "Shopping", AccountName = "Monzo" },

                // Month -5
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddMonths(-5).AddDays(-3), Description = "Uber Eats", Amount = -22.50m, Category = "Eating Out", AccountName = "Monzo" },
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddMonths(-5).AddDays(-9), Description = "Pharmacy", Amount = -12.20m, Category = "Health", AccountName = "Santander" },
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddMonths(-5).AddDays(-15), Description = "Cinema", Amount = -13.00m, Category = "Entertainment", AccountName = "Monzo" },

                // Month -7
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddMonths(-7).AddDays(-1), Description = "Cafe Coffee", Amount = -7.20m, Category = "Eating Out", AccountName = "Monzo" },
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddMonths(-7).AddDays(-5), Description = "Bookstore", Amount = -18.00m, Category = "Shopping", AccountName = "Santander" },
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddMonths(-7).AddDays(-12), Description = "Movie Ticket", Amount = -12.00m, Category = "Entertainment", AccountName = "Monzo" },

                // Month -10
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddMonths(-10).AddDays(-4), Description = "Cafe Coffee", Amount = -7.20m, Category = "Eating Out", AccountName = "Monzo" },
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddMonths(-10).AddDays(-8), Description = "Freelance Pay", Amount = 820.00m, Category = "Income", AccountName = "Santander" },
                new Transaction { Id = Guid.NewGuid(), Date = DateTime.Now.AddMonths(-10).AddDays(-14), Description = "Groceries", Amount = -32.75m, Category = "Groceries", AccountName = "Santander" },
            };

            return Task.FromResult<IEnumerable<Transaction>>(transactions);
        }
    }
}