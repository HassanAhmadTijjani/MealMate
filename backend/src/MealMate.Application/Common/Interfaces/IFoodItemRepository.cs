using MealMate.Domain.Entities;

namespace MealMate.Application.Common.Interfaces;
public interface IFoodItemRepository
{
    Task<List<FoodItem>> GetAllAsync();
    Task<FoodItem?> GetByIdAsync(Guid id);
    Task AddAsync(FoodItem foodItem);
    Task UpdateAsync(FoodItem foodItem);
    Task DeleteAsync(FoodItem foodItem);
}