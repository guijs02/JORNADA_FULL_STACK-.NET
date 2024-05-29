using FinaFlow.Shared.Models.Enums;

namespace FinaFlow.Shared.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public DateTime? PaidOrReceivedAt { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ETransactionType Type { get; set; } = ETransactionType.Withdraw;
        public string UserId { get; set; }
    }
}