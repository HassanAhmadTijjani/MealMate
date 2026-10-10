using MealMate.Domain.Enums;

namespace MealMate.Application.Features.Orders.Commands.Checkout;

public sealed record CheckoutRequest(
OrderType OrderType,
string? DeliveryAddress

);