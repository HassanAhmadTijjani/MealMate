using MealMate.Application.Common.Interfaces;
using MealMate.Domain.Entities;
using MealMate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MealMate.Infrastructure.Repositories;
public class PaymentSettingsRepository(MealMateDbContext context): IPaymentSettingsRepository
{
    private readonly MealMateDbContext _context = context;
    public async Task AddAsync(PaymentSettings paymentSettings)
    {
        await _context.PaymentSettings.AddAsync(paymentSettings);
        await _context.SaveChangesAsync();
    }

    public async Task<PaymentSettings?> GetAsync()
    {
        return await _context.PaymentSettings.FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(PaymentSettings paymentSettings)
    {
        await _context.SaveChangesAsync();
    }
}