namespace MealMate.Application.Common.Models.PaymentSetting;

public sealed record PaymentSettingsRequest(
string BankName,
string AccountName,
string AccountNumber,
string? PaymentInstructions
);