using BudgetTracker.Api.Models;

namespace BudgetTracker.Api.Data
{
    public static class DatabaseSeeder
    {
        public static void Seed(BudgetTrackerContext context)
        {
            // Clear all existing transactions (for DEMO purposes)
            context.Transactions.RemoveRange(context.Transactions);
            context.SaveChanges();

            var transactions = new List<Transaction>
            {
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2025, 12, 2), Description = "Salary", Amount = 3480.00m, Category = "Income", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2025, 12, 5), Description = "Tesco", Amount = -44.20m, Category = "Groceries", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2025, 12, 8), Description = "Trainline", Amount = -11.40m, Category = "Transport", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2025, 12, 11), Description = "Netflix", Amount = -17.99m, Category = "Entertainment", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2025, 12, 14), Description = "Deliveroo", Amount = -24.50m, Category = "Eating Out", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2025, 12, 19), Description = "Utilities Bill", Amount = -102.00m, Category = "Housing", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2025, 12, 24), Description = "Amazon", Amount = -39.99m, Category = "Shopping", AccountName = "Current Account" },

                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 1, 2), Description = "Salary", Amount = 3510.00m, Category = "Income", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 1, 6), Description = "Sainsbury's", Amount = -41.75m, Category = "Groceries", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 1, 9), Description = "Trainline", Amount = -10.80m, Category = "Transport", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 1, 12), Description = "Spotify", Amount = -11.99m, Category = "Entertainment", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 1, 16), Description = "Pret", Amount = -15.30m, Category = "Eating Out", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 1, 21), Description = "Internet", Amount = -32.00m, Category = "Housing", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 1, 26), Description = "Boots", Amount = -23.49m, Category = "Shopping", AccountName = "Current Account" },

                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 2, 2), Description = "Salary", Amount = 3475.00m, Category = "Income", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 2, 4), Description = "Tesco", Amount = -47.10m, Category = "Groceries", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 2, 8), Description = "Trainline", Amount = -9.60m, Category = "Transport", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 2, 10), Description = "Netflix", Amount = -17.99m, Category = "Entertainment", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 2, 13), Description = "Deliveroo", Amount = -21.20m, Category = "Eating Out", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 2, 18), Description = "Gym Membership", Amount = -35.00m, Category = "Health", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 2, 23), Description = "Amazon", Amount = -28.99m, Category = "Shopping", AccountName = "Current Account" },

                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 3, 2), Description = "Salary", Amount = 3520.00m, Category = "Income", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 3, 5), Description = "Sainsbury's", Amount = -39.30m, Category = "Groceries", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 3, 7), Description = "Trainline", Amount = -12.20m, Category = "Transport", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 3, 12), Description = "Spotify", Amount = -11.99m, Category = "Entertainment", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 3, 15), Description = "Pret", Amount = -18.40m, Category = "Eating Out", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 3, 19), Description = "Utilities Bill", Amount = -95.00m, Category = "Housing", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 3, 25), Description = "Boots", Amount = -31.25m, Category = "Shopping", AccountName = "Current Account" },

                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 4, 2), Description = "Salary", Amount = 3500.00m, Category = "Income", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 4, 6), Description = "Tesco", Amount = -45.30m, Category = "Groceries", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 4, 7), Description = "Trainline", Amount = -8.40m, Category = "Transport", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 4, 10), Description = "Netflix", Amount = -17.99m, Category = "Entertainment", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 4, 12), Description = "Deliveroo", Amount = -22.50m, Category = "Eating Out", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 4, 18), Description = "Gym Membership", Amount = -35.00m, Category = "Health", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 4, 22), Description = "Amazon", Amount = -29.99m, Category = "Shopping", AccountName = "Current Account" },

                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 5, 2), Description = "Salary", Amount = 3490.00m, Category = "Income", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 5, 5), Description = "Sainsbury's", Amount = -42.60m, Category = "Groceries", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 5, 6), Description = "Trainline", Amount = -12.60m, Category = "Transport", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 5, 9), Description = "Spotify", Amount = -11.99m, Category = "Entertainment", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 5, 14), Description = "Pret", Amount = -19.15m, Category = "Eating Out", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 5, 20), Description = "Internet", Amount = -32.00m, Category = "Housing", AccountName = "Current Account" },
                new Transaction { Id = Guid.NewGuid(), Date = new DateTime(2026, 5, 24), Description = "Boots", Amount = -26.75m, Category = "Shopping", AccountName = "Current Account" },
            };

            context.Transactions.AddRange(transactions);
            context.SaveChanges();
        }
    }
}