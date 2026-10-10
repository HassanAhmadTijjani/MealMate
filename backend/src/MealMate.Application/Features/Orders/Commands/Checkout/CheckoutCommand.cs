using MealMate.Application.Common.Models.Orders;
using MealMate.Domain.Enums;
using MediatR;

namespace MealMate.Application.Features.Orders.Commands.Checkout;

public sealed record CheckoutCommand(OrderType OrderType, string? DeliveryAddress) : IRequest<OrderResponse>;