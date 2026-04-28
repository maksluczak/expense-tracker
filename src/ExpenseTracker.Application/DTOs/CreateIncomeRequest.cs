namespace ExpenseTracker.Application.DTOs;

public record CreateIncomeRequest(
    decimal Amount,
    string Description,
    DateTime Date
);