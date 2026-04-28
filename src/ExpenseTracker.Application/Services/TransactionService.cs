using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Application.Services;

public class TransactionService(IApplicationDbContext context) : ITransactionService
{
    public async Task<Guid> CreateExpenseAsync(CreateExpenseRequest request)
    {
        var expense = new Expense
        {
            Amount = request.Amount,
            Description = request.Description,
            ExpenseType = request.ExpenseType,
            TransactionType =  TransactionType.Expense,
            CreatedAt = DateTime.UtcNow
        };

        context.Expenses.Add(expense);
        await context.SaveChangesAsync();
        
        return expense.Id;
    }
    
    public async Task<IEnumerable<TransactionResponse>> GetAllTransactionsAsync()
    {
        return await context.Transactions
            .AsQueryable()
            .Select(t => new TransactionResponse(
                t.Id,
                t.Amount,
                t.Description,
                t.TransactionType.ToString(),
                t is Expense ? ((Expense)t).ExpenseType.ToString() : null,
                t.CreatedAt
            )).ToListAsync();
    }
}