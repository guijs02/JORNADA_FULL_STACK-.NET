using FinaFlow.Shared.Models;
using FinaFlow.Shared.Models.Enums;

namespace FinaFlow.Shared.Requests.Transactions
{
    public class CreateTransactionRequest : Request
    {
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public DateTime? PaidOrReceivedAt { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ETransactionType Type { get; set; } = ETransactionType.Withdraw;
    }
}
