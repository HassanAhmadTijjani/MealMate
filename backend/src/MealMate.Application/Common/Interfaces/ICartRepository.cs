using MealMate.Domain.Entities;

namespace MealMate.Application.Common.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetByCustomerIdAsync(string customerId);

    Task AddAsync(Cart cart);

    Task UpdateAsync(Cart cart);
}