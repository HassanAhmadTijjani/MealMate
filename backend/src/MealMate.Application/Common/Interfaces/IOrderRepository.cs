using MealMate.Domain.Entities;

namespace MealMate.Application.Common.Interfaces;
public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order?> GetByIdAsync(Guid orderId);
    Task<List<Order>> GetByCustomerIdAsync(string customerId);
    Task UpdateAsync(Order order);

}