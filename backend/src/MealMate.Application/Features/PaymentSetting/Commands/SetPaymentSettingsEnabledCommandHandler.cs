
using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.MAppings;
using MealMate.Application.Common.Models.PaymentSetting;
using MediatR;

namespace MealMate.Application.Features.PaymentSetting.Commands;

public sealed class SetPaymentSettingsEnabledCommandHandler(
    IPaymentSettingsRepository paymentSettingsRepository)
    : IRequestHandler<SetPaymentSettingsEnabledCommand, PaymentSettingsResponse>
{
    private readonly IPaymentSettingsRepository _paymentSettingsRepository =
        paymentSettingsRepository;

    public async Task<PaymentSettingsResponse> Handle(
        SetPaymentSettingsEnabledCommand request,
        CancellationToken cancellationToken)
    {
        var paymentSettings = await _paymentSettingsRepository.GetAsync()
            ?? throw new ArgumentException(
                "Payment settings have not been configured yet.");

        paymentSettings.SetEnabledStatus(request.IsEnabled);

        await _paymentSettingsRepository.UpdateAsync(paymentSettings);

        return paymentSettings.ToResponse();
    }
}
