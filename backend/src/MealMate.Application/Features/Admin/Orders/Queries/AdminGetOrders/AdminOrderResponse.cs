using MealMate.Domain.Enums;

namespace MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrders;

public sealed record AdminOrderResponse(
Guid Id,
string OrderRefrence,
string CustomerName,
string CustomerEmail,
string? CustomerPhone,
OrderType OrderType,
OrderStatus OrderStatus,
PaymentStatus PaymentStatus,
decimal TotalAmount,
int TotalItems,
DateTime CreatedAt
);