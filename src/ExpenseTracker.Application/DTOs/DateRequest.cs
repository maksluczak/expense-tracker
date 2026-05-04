namespace ExpenseTracker.Application.DTOs;

public record DateRequest(
    int? Year,
    int? Month
);