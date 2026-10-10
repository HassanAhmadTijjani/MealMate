using MealMate.Application.Common.Interfaces;
using MealMate.Domain.Entities;
using MealMate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MealMate.Infrastructure.Repositories;

public class OrderRepository(MealMateDbContext context) : IOrderRepository
{
    private readonly MealMateDbContext _context = context;
    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Order>> GetByCustomerIdAsync(string customerId)
    {
        return await _context.Orders.Include(order => order.Items).Where(order => order.CustomerId == customerId).OrderByDescending(order => order.CreatedAt).ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid orderId)
    {
        return await _context.Orders.Include(order => order.Items).FirstOrDefaultAsync(order => order.Id == orderId);
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
}