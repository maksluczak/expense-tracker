using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Domain.Entities;

public class Expense : Transaction
{
    public required ExpenseType ExpenseType { get; set; }
    
    public Expense()
    {
        TransactionType = TransactionType.Expense;
    }
}