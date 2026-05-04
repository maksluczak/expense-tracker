using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Endpoints;

public static class TransactionEndpoints
{
    public static void MapTransactionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/transactions")
            .WithTags("Transactions");
        
        group.MapPost("/expense", async ([FromBody] CreateExpenseRequest request, ITransactionService transactionService) =>
        {
            var id = await transactionService.CreateExpenseAsync(request);
            return Results.Created($"/api/transactions/{id}", id);
        });

        group.MapPost("/income", async ([FromBody] CreateIncomeRequest request, ITransactionService transactionService) =>
        {
            var id = await transactionService.CreateIncomeAsync(request);
            return Results.Created($"/api/transactions/{id}", id);
        });
        
        group.MapGet("/", async (ITransactionService transactionService) =>
        {
            var transactions = await transactionService.GetAllTransactionsAsync();
            return Results.Ok(transactions);
        });

        group.MapGet("/monthly", async ([AsParameters] DateRequest request, ITransactionService transactionService) =>
        {
            var transactions = await transactionService.GetAllTransactionsFromOneMonthAsync(request);
            return Results.Ok(transactions);
        });
        
        group.MapGet("/balance", async (ITransactionService transactionService) =>
        {
            var transactions = await transactionService.CalculateBalanceForEveryMonthInYearAsync();
            return Results.Ok(transactions);
        });
    }
}