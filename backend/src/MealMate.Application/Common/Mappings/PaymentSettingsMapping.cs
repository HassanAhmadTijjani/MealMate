using MealMate.Application.Common.Models.PaymentSetting;
using MealMate.Domain.Entities;

namespace MealMate.Application.Common.MAppings;
public static class PaymentSettingsMapping
{
    public static PaymentSettingsResponse ToResponse(this PaymentSettings paymentSettings) => new(
         paymentSettings.Id,
            paymentSettings.BankName,
            paymentSettings.AccountName,
            paymentSettings.AccountNumber,
            paymentSettings.PaymentInstructions,
            paymentSettings.IsEnabled,
            paymentSettings.CreatedAt,
            paymentSettings.UpdatedAt
    );
}