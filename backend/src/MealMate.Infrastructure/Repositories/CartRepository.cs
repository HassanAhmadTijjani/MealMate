using MealMate.Application.Common.Interfaces;
using MealMate.Domain.Entities;
using MealMate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MealMate.Infrastructure.Repositories;

public class CartRepository(MealMateDbContext context) : ICartRepository
{
    private readonly MealMateDbContext _context = context;
    public async Task AddAsync(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
        await _context.SaveChangesAsync();
  }

    public async Task<Cart?> GetByCustomerIdAsync(string customerId)
    {
      return await _context.Carts.Include(cart => cart.Items).ThenInclude(item => item.FoodItem).Include(userId => userId.Id).FirstOrDefaultAsync(cart => cart.CustomerId == customerId);  
    }

    public async Task UpdateAsync(Cart cart)
    {
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync();
    }
}