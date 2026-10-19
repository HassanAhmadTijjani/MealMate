using MealMate.Domain.Entities;

namespace MealMate.Application.Common.Interfaces;
public interface IPaymentSettingsRepository
{
    Task AddAsync(PaymentSettings paymentSettings);
    Task<PaymentSettings?> GetAsync();
    Task UpdateAsync(PaymentSettings paymentSettings);
}