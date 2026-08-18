using MealMate.Domain.Entities;

namespace MealMate.Application.Common.Interfaces;
public interface IRestaurantRepository
{
    Task AddAsync(Restaurant restaurant);
    Task<IReadOnlyList<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(Guid id);
    Task UpdateAsync(Restaurant restaurant);
    Task DeleteAsync(Restaurant restaurant);

}