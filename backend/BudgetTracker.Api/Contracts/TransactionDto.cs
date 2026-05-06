namespace BudgetTracker.Api.Contracts
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public DateTime DateUtc { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal SignedAmount { get; set; }
        public decimal AbsoluteAmount { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public string MonthKey { get; set; } = string.Empty;
        public string DateKey { get; set; } = string.Empty;
    }
}
