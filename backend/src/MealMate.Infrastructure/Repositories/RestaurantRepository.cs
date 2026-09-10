using MealMate.Application.Common.Interfaces;
using MealMate.Domain.Entities;
using MealMate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MealMate.Infrastructure.Repositories;

public class RestaurantRepository(MealMateDbContext context)
    : IRestaurantRepository
{
    private readonly MealMateDbContext _context = context;

    public async Task<Restaurant?> GetAsync()
    {
        return await _context.Restaurants.AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task AddAsync(Restaurant restaurant)
    {
        await _context.Restaurants.AddAsync(restaurant);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Restaurant restaurant)
    {
        _context.Restaurants.Update(restaurant);
        await _context.SaveChangesAsync();
    }
}