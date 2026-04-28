using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.DTOs;

public record CreateExpenseRequest(
    decimal Amount,
    string Description,
    ExpenseType ExpenseType,
    DateTime Date
);