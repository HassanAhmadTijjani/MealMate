using MealMate.Application.Common.Models.Orders;
using MealMate.Domain.Enums;
using MediatR;

namespace MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder;

public sealed record GetAdminOrderQuery(Guid OrderId) : IRequest<AdminOrderResponse>;