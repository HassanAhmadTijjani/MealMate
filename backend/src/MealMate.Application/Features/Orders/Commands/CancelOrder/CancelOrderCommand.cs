using MealMate.Application.Common.Models.Orders;
using MediatR;

namespace MealMate.Application.Features.Orders.Commands.CancelOrder;

public sealed record CancelOrderCommand(Guid OrderId) : IRequest<OrderResponse>;