using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.MAppings;
using MealMate.Application.Common.Models.PaymentSetting;
using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.PaymentSetting.Commands;

public sealed class UpsertPaymentSettingsCommandHandler(IPaymentSettingsRepository PaymentSettingsRepository) : IRequestHandler<UpsertPaymentSettingsCommand, PaymentSettingsResponse>
{
    private readonly IPaymentSettingsRepository _paymentSettingsRepository = PaymentSettingsRepository;
    public async Task<PaymentSettingsResponse> Handle(UpsertPaymentSettingsCommand request, CancellationToken cancellationToken)
    {
        var paymentSettings = await _paymentSettingsRepository.GetAsync();
        if(paymentSettings is null)
        {
            // Create new
            paymentSettings = new PaymentSettings(
                 request.BankName,
                 request.AccountName,
                 request.AccountNumber,
                 request.PaymentInstructions
            );
            await _paymentSettingsRepository.AddAsync(paymentSettings);
        } else
        {
            // Update existing setting
            paymentSettings.UpdateDetails(
                request.BankName,
                request.AccountName,
                request.AccountNumber,
                request.PaymentInstructions
            );
            await _paymentSettingsRepository.UpdateAsync(paymentSettings);
        }
        return paymentSettings.ToResponse();
    }
}