using MealMate.Application.Common.Models.Orders;
using MediatR;

namespace MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Commands.Completed;

public sealed record CompleteOrderCommand(Guid OrderId) : IRequest<OrderResponse>;
