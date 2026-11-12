using MealMate.Domain.Enums;
using MediatR;

namespace MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrders;

public sealed record GetAdminOrdersQuery(OrderStatus? Status = null) : IRequest<List<AdminOrdersResponse>>;