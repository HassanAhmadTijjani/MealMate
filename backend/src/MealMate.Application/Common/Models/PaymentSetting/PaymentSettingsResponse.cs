namespace MealMate.Application.Common.Models.PaymentSetting;

public sealed record PaymentSettingsResponse(
Guid Id,
string BankName,
string AccountName,
string AccountNumber,
string? PaymentInstructions,
bool IsEnabled,
DateTime CreatedAt,
DateTime? UpdatedAt
);