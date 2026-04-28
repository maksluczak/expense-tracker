using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Transaction> Transactions { get; }
    DbSet<Expense> Expenses { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}