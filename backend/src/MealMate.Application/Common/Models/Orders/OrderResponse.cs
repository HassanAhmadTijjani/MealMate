using MealMate.Domain.Enums;

namespace MealMate.Application.Common.Models.Orders;

public sealed record OrderResponse(
    Guid Id,
string OrderReference,
    OrderType OrderType,
    OrderStatus OrderStatus,
    PaymentStatus PaymentStatus,
    string? DeliveryAddress,
    List<OrderItemResponse> Items,
    decimal ItemsSubtotal,
    decimal DeliveryFee,
    decimal TotalAmount,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    DateTime PaymentExpiresAt
);