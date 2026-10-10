using MealMate.Application.Common.Models.Orders;
using MediatR;

namespace MealMate.Application.Features.Orders.Queries.GetCustomerOrders;

public sealed record GetCustomerOrdersQuery : IRequest<List<OrderResponse>>;