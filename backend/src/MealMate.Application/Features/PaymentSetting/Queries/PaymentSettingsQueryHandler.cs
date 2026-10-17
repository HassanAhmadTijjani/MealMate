using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.MAppings;
using MealMate.Application.Common.Models.PaymentSetting;
using MediatR;

namespace MealMate.Application.Features.PaymentSetting.Queries;

public sealed record PaymentSettingsQueryHandler(IPaymentSettingsRepository PaymentSettingsRepository) : IRequestHandler<PaymentSettingsQuery, PaymentSettingsResponse?>
{
    private readonly IPaymentSettingsRepository _paymentSettingsRepository = PaymentSettingsRepository;
    public async Task<PaymentSettingsResponse?> Handle(PaymentSettingsQuery request, CancellationToken cancellationToken)
    {
        var paymentSettings = await _paymentSettingsRepository.GetAsync();
        // if (paymentSettings is null) return null;
        return paymentSettings?.ToResponse();
    }
}