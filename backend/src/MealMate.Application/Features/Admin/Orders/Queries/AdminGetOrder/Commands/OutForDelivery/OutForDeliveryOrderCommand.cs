using MealMate.Application.Common.Models.Orders;
using MediatR;

namespace MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Commands.OutForDelivery;

public sealed record OutForDeliveryOrderCommand(Guid OrderId) : IRequest<OrderResponse>;
