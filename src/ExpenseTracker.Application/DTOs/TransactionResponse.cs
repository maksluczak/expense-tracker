using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.DTOs;

public record TransactionResponse(
    Guid Id,
    decimal Amount,
    string Description,
    string TransactionType,
    string? ExpenseType,
    DateTime Date
);