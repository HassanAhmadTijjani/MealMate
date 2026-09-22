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
        return await _context.Carts
            .Include(cart => cart.Items)
                .ThenInclude(item => item.FoodItem)
            .FirstOrDefaultAsync(cart => cart.CustomerId == customerId);
    }

    public async Task UpdateAsync(Cart cart)
    {
        foreach (var item in cart.Items)
        {
            var entry = _context.Entry(item);

            if (entry.State == EntityState.Modified &&
                !await _context.CartItems.AnyAsync(
                    cartItem => cartItem.Id == item.Id))
            {
                entry.State = EntityState.Added;
            }
        }

        await _context.SaveChangesAsync();
    }
}