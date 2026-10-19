using MediatR;

namespace MealMate.Application.Common.Models.PaymentSetting;

public sealed record PaymentSettingsEnabledRequest(bool IsEnabled);