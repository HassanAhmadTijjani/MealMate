using MealMate.Application.Common.Models.Orders;
using MediatR;

namespace MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Commands.MarkAsPaid;

public sealed record MarkOrderAsPaidCommand(Guid OrderId) : IRequest<OrderResponse>;