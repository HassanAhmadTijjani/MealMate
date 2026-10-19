using MealMate.Application.Common.Models.PaymentSetting;
using MediatR;

namespace MealMate.Application.Features.PaymentSetting.Commands;

public sealed record UpsertPaymentSettingsCommand(string BankName, string AccountName, string AccountNumber, string? PaymentInstructions) : IRequest<PaymentSettingsResponse>;