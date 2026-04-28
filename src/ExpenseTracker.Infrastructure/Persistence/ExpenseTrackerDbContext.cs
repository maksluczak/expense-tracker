using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Infrastructure.Persistence;

public class ExpenseTrackerDbContext : DbContext, IApplicationDbContext
{
    public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : base(options) { }
    
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Expense> Expenses => Set<Expense>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Transaction>()
            .HasDiscriminator<TransactionType>("TransactionType")
            .HasValue<Transaction>(TransactionType.Income)
            .HasValue<Expense>(TransactionType.Expense);

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Amount)
                .HasPrecision(18, 2)
                .IsRequired();
            
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsRequired(false);
            
            entity.Property(e => e.CreatedAt)
                .IsRequired();
        });
        
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.Property(e => e.ExpenseType)
                .IsRequired();
        });
    }
}