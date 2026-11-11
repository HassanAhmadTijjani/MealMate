using MealMate.Application.Common.Models.Orders;
using MealMate.Domain.Enums;

namespace MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder;

public sealed record AdminOrderResponse(
Guid Id,
string OrderReference,
string? CustomerName,
string? CustomerEmail,
string? CustomerPhone,
OrderType OrderType,
string? DeliveryAddress,
OrderStatus OrderStatus,
PaymentStatus PaymentStatus,
List<OrderItemResponse> Items,
decimal ItemsSubtotal,
decimal DeliveryFee,
decimal TotalAmount,
int TotalItems,
DateTime CreatedAt
);