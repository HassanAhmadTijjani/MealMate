using MealMate.Domain.Entities;

namespace MealMate.Application.Common.Interfaces;
public interface IRestaurantRepository
{
    Task<Restaurant?> GetAsync();

    Task AddAsync(Restaurant restaurant);

    Task UpdateAsync(Restaurant restaurant);
}