using BudgetTracker.Api.Models;

namespace BudgetTracker.Api.Data
{
    public static class DatabaseSeeder
    {
        public static void Seed(BudgetTrackerContext context)
        {
            if (context.Transactions.Any())
                return;

            var transactions = new List<Transaction>
            {
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 4, 1), Description = "Tesco", Amount = -45.30m, Category = "Groceries", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 4, 2), Description = "Spotify", Amount = -11.99m, Category = "Entertainment", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 4, 3), Description = "Salary", Amount = 3500.00m, Category = "Income", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 4, 5), Description = "Deliveroo", Amount = -22.50m, Category = "Eating Out", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 4, 7), Description = "TfL", Amount = -8.40m, Category = "Transport", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 4, 10), Description = "Netflix", Amount = -17.99m, Category = "Entertainment", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 4, 12), Description = "Sainsbury's", Amount = -38.75m, Category = "Groceries", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 4, 15), Description = "Rent", Amount = -1200.00m, Category = "Housing", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 4, 18), Description = "Gym", Amount = -35.00m, Category = "Health", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 4, 22), Description = "Amazon", Amount = -29.99m, Category = "Shopping", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 5, 1), Description = "Tesco", Amount = -52.10m, Category = "Groceries", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 5, 2), Description = "Salary", Amount = 3500.00m, Category = "Income", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 5, 4), Description = "Deliveroo", Amount = -18.75m, Category = "Eating Out", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 5, 6), Description = "TfL", Amount = -12.60m, Category = "Transport", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 5, 7), Description = "Spotify", Amount = -11.99m, Category = "Entertainment", AccountName = "Current Account" },
            };

            context.Transactions.AddRange(transactions);
            context.SaveChanges();
        }
    }
}