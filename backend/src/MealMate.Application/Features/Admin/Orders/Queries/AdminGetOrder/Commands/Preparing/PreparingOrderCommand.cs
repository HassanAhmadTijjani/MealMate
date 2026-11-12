using MealMate.Application.Common.Models.Orders;
using MediatR;

namespace MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Commands.Preparing;

public sealed record PreparingOrderCommand(Guid OrderId) : IRequest<OrderResponse>;