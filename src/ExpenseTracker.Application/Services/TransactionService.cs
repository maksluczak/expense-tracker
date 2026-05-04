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

    public async Task<Guid> CreateIncomeAsync(CreateIncomeRequest request)
    {
        var income = new Transaction
        {
            Amount = request.Amount,
            Description = request.Description,
            TransactionType =  TransactionType.Income,
            CreatedAt = DateTime.UtcNow
        };
        
        context.Transactions.Add(income);
        await context.SaveChangesAsync();
        
        return income.Id;
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

    public async Task<IEnumerable<TransactionResponse>> GetAllTransactionsFromOneMonthAsync(DateRequest request)
    {
        var year = request.Year ?? DateTime.Now.Year;
        var month = request.Month ?? DateTime.Now.Month;

        var beginOfMonth = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
        var beginOfNextMonth = beginOfMonth.AddMonths(1);

        return await context.Transactions
            .Where(t => t.CreatedAt >= beginOfMonth && t.CreatedAt < beginOfNextMonth)
            .Select(t => new TransactionResponse(
                t.Id,
                t.Amount,
                t.Description,
                t.TransactionType.ToString(),
                t is Expense ? ((Expense)t).ExpenseType.ToString() : null,
                t.CreatedAt
            ))
            .ToListAsync();
    }

    public async Task<IEnumerable<CalculatedBalanceResponse>> CalculateBalanceForEveryMonthInYearAsync()
    {
        return await context.Transactions
            .GroupBy(t => new { t.CreatedAt.Year, t.CreatedAt.Month })
            .Select(g => new CalculatedBalanceResponse(
                g.Key.Year,
                g.Key.Month,
                g.Sum(t => (t is Expense) ? -t.Amount :  t.Amount)
            ))
            .OrderByDescending(x => x.Month)
            .ToListAsync();
    }
}