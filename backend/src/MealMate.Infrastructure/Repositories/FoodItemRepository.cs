using MealMate.Application.Common.Interfaces;
using MealMate.Domain.Entities;
using MealMate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MealMate.Infrastructure.Repositories;
public class FoodItemRepository(MealMateDbContext context) : IFoodItemRepository
{
    private readonly MealMateDbContext _context = context;
    public async Task<List<FoodItem>> GetAllAsync()
    {
        return await _context.FoodItems.AsNoTracking().Include(foodItem => foodItem.Category).ToListAsync();
    }

    public async Task<FoodItem?> GetByIdAsync(Guid id)
    {
        return await _context.FoodItems.AsNoTracking().Include(foodItem => foodItem.Category).FirstOrDefaultAsync(foodItem => foodItem.Id == id);
    }

    public async Task AddAsync(FoodItem foodItem)
    {
        await _context.FoodItems.AddAsync(foodItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(FoodItem foodItem)
    {
        _context.FoodItems.Update(foodItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(FoodItem foodItem)
    {
        _context.FoodItems.Remove(foodItem);
        await _context.SaveChangesAsync();
    }

    public async Task<List<FoodItem>> GetAvailableAsync(Guid? categoryId)
    {
        var query = _context.FoodItems.AsNoTracking().Include(foodItem => foodItem.Category).Where(foodItem => foodItem.IsAvailable);
        if (categoryId.HasValue) query = query.Where(foodItem => foodItem.CategoryId == categoryId.Value);
        return await query.ToListAsync();
    }
}