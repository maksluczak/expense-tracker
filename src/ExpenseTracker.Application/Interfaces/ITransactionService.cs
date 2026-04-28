using ExpenseTracker.Application.DTOs;

namespace ExpenseTracker.Application.Interfaces;

public interface ITransactionService
{
    Task<Guid> CreateExpenseAsync(CreateExpenseRequest request);
    Task<IEnumerable<TransactionResponse>> GetAllTransactionsAsync();
}