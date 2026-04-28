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
        
        group.MapGet("/", async (ITransactionService transactionService) =>
        {
            var transactions = await transactionService.GetAllTransactionsAsync();
            return Results.Ok(transactions);
        });
        
        group.MapPost("/expense", async ([FromBody] CreateExpenseRequest request, ITransactionService transactionService) =>
        {
            var id = await transactionService.CreateExpenseAsync(request);
            return Results.Created($"/api/transactions/{id}", id);
        });
    }
}