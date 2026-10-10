using MealMate.Application.Common.Models.Orders;
using MediatR;

namespace MealMate.Application.Features.Orders.Queries.GetCustomerOrder;

public sealed record GetCustomerOrderQuery(
    Guid OrderId
) : IRequest<OrderResponse>;