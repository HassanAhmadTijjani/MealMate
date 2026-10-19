
using MealMate.Application.Common.Models.PaymentSetting;
using MediatR;

namespace MealMate.Application.Features.PaymentSetting.Commands;

public sealed record SetPaymentSettingsEnabledCommand(
    bool IsEnabled
) : IRequest<PaymentSettingsResponse>;
