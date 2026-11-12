using MealMate.Application.Common.Models.Orders;
using MediatR;

namespace MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Commands.MarkAsFailed;

public sealed record MarkOrderAsFailedCommand(
    Guid OrderId,
    string Reason
) : IRequest<OrderResponse>;
