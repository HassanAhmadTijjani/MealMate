using MediatR;

namespace MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrders;

public sealed record GetAdminOrdersQuery : IRequest<List<AdminOrdersResponse>>;