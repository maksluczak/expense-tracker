namespace ExpenseTracker.Application.DTOs;

public record CalculatedBalanceResponse(
    int Year,
    int Month,
    decimal Amount
);