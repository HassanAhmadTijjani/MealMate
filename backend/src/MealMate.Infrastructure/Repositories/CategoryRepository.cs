using MealMate.Application.Common.Interfaces;
using MealMate.Domain.Entities;
using MealMate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MealMate.Infrastructure.Repositories;

public class CategoryRepository(MealMateDbContext context)
    : ICategoryRepository
{
    private readonly MealMateDbContext _context = context;

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(category => category.Id == id);
    }

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}