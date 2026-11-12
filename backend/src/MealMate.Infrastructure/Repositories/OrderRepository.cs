using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.Orders;
using MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder;
using MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrders;
using MealMate.Domain.Entities;
using MealMate.Domain.Enums;
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
    public async Task<Order?> GetByIdWithPaymentSubmissionsAsync(
    Guid orderId)
    {
        return await _context.Orders
            .Include(order => order.Items)
            .Include(order => order.PaymentSubmissions)
            .FirstOrDefaultAsync(order => order.Id == orderId);
    }

    public async Task SavePaymentSubmissionAsync(
        Order order,
        PaymentSubmission submission)
    {
        await _context.PaymentSubmissions.AddAsync(submission);

        // The order is already tracked by this DbContext.
        // One SaveChanges persists both changes atomically.
        await _context.SaveChangesAsync();
    }

    public async Task<List<AdminOrdersResponse>> GetAllForAdminAsync(CancellationToken cancellationToken)
    {
        var query = from order in _context.Orders.AsNoTracking()
                    join user in _context.Users.AsNoTracking()
                    on order.CustomerId equals user.Id into users
                    from customer in users.DefaultIfEmpty()
                    orderby order.CreatedAt descending
                    select new AdminOrdersResponse(
                        order.Id,
                        order.OrderReference,
                        customer == null ? null : (customer.FirstName + " " + customer.LastName).Trim(),
                        customer == null ? null : customer.Email,
                        customer == null ? null : customer.PhoneNumber,
                        order.OrderType,
                        order.OrderStatus,
                        order.PaymentStatus,
                        order.TotalAmount,
                        order.Items.Sum(item => (int?)item.Quantity) ?? 0,
                        order.CreatedAt);
        return await query.ToListAsync(cancellationToken);

    }
    public async Task<AdminOrderResponse?> GetByIdForAdminAsync(
        Guid orderId,
        CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .AsNoTracking()
            .Include(order => order.Items)
            .FirstOrDefaultAsync(order => order.Id == orderId, cancellationToken);

        if (order is null)
        {
            return null;
        }

        var customer = await _context.Users
            .AsNoTracking()
            .Where(user => user.Id == order.CustomerId)
            .Select(user => new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                user.PhoneNumber
            })
            .FirstOrDefaultAsync(cancellationToken);

        var items = order.Items
            .Select(item => new OrderItemResponse(
                item.FoodItemId,
                item.FoodItemName,
                item.UnitPrice,
                item.Quantity,
                item.Subtotal))
            .ToList();

        return new AdminOrderResponse(
            order.Id,
            order.OrderReference,
            customer is null
                ? null
                : $"{customer.FirstName} {customer.LastName}".Trim(),
            customer?.Email,
            customer?.PhoneNumber,
            order.OrderType,
            order.DeliveryAddress,
            order.OrderStatus,
            order.PaymentStatus,
            items,
            order.ItemsSubtotal,
            order.DeliveryFee,
            order.TotalAmount,
            order.Items.Sum(item => item.Quantity),
            order.CreatedAt);
    }

    public async Task<List<AdminOrdersResponse>> GetAllForAdminAsync(OrderStatus? status, CancellationToken cancellationToken)
    {
        var query = from order in _context.Orders.AsNoTracking()
                    join user in _context.Users.AsNoTracking()
                    on order.CustomerId equals user.Id into users
                    from customer in users.DefaultIfEmpty()
                    where !status.HasValue || order.OrderStatus == status.Value
                    orderby order.CreatedAt descending
                    select new AdminOrdersResponse(
                    order.Id,
                    order.OrderReference,
                    customer == null ? null : (customer.FirstName + " " + customer.LastName).Trim(),
                    customer == null ? null : customer.Email,
                    customer == null ? null : customer.PhoneNumber,
                    order.OrderType,
                    order.OrderStatus,
                    order.PaymentStatus,
                    order.TotalAmount,
                    order.Items.Sum(item => (int?)item.Quantity) ?? 0,
                    order.CreatedAt
                    );
        return await query.ToListAsync(cancellationToken);
    }
}