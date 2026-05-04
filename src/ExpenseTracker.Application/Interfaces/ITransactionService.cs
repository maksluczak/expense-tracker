using ExpenseTracker.Application.DTOs;

namespace ExpenseTracker.Application.Interfaces;

public interface ITransactionService
{
    Task<Guid> CreateExpenseAsync(CreateExpenseRequest request);
    Task<Guid> CreateIncomeAsync(CreateIncomeRequest request);
    Task<IEnumerable<TransactionResponse>> GetAllTransactionsAsync();
    Task<IEnumerable<TransactionResponse>> GetAllTransactionsFromOneMonthAsync(DateRequest request);
    Task<IEnumerable<CalculatedBalanceResponse>> CalculateBalanceForEveryMonthInYearAsync();
}