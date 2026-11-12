using MealMate.Application.Common.Models.Orders;
using MediatR;

namespace MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Commands.Confirm;

public sealed record ConfirmOrderCommand(Guid OrderId) : IRequest<OrderResponse>;