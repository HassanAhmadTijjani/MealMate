using MealMate.Application.Common.Interfaces;
using MealMate.Domain.Entities;
using MealMate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MealMate.Infrastructure.Repositories;

public class RestaurantRepository(MealMateDbContext context) : IRestaurantRepository
{
    private readonly MealMateDbContext _context = context;

    public async Task AddAsync(Restaurant restaurant)
    {
        await _context.Restaurants.AddAsync(restaurant);
        await _context.SaveChangesAsync();
    }
    public async Task<IReadOnlyList<Restaurant>> GetAllAsync()
    {
        return await _context.Restaurants.AsNoTracking().ToListAsync();
    }
    public async Task<Restaurant?> GetByIdAsync(Guid id)
    {
        return await _context.Restaurants.AsNoTracking().FirstOrDefaultAsync(restaurant => restaurant.Id == id);
    }
    public async Task UpdateAsync(Restaurant restaurant)
    {
        _context.Update(restaurant);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Restaurant restaurant)
    {
         _context.Remove(restaurant);
        await _context.SaveChangesAsync();
    }
}