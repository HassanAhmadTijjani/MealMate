using MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder;
using MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrders;
using MealMate.Domain.Entities;
using MealMate.Domain.Enums;

namespace MealMate.Application.Common.Interfaces;
public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order?> GetByIdAsync(Guid orderId);
    Task<List<Order>> GetByCustomerIdAsync(string customerId);
    Task UpdateAsync(Order order);
    Task<Order?> GetByIdWithPaymentSubmissionsAsync(Guid orderId);
    Task SavePaymentSubmissionAsync(Order order, PaymentSubmission submission);
    Task<List<AdminOrdersResponse>> GetAllForAdminAsync(OrderStatus? status, CancellationToken cancellationToken);
    Task<AdminOrderResponse?> GetByIdForAdminAsync(
    Guid orderId,
    CancellationToken cancellationToken);

}