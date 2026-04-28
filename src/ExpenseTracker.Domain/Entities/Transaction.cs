using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required TransactionType TransactionType { get; set; }
    public required decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}