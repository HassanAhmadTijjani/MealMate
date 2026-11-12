using MealMate.Application.Common.Models.Orders;
using MediatR;

namespace MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Commands.Ready;

public sealed record ReadyOrderCommand(Guid OrderId) : IRequest<OrderResponse>;