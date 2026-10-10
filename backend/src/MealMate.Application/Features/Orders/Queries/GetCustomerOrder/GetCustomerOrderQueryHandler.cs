using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.Orders;
using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.Orders.Queries.GetCustomerOrder;

public sealed record GetCustomerOrderQueryHandler(
    IOrderRepository OrderRepository,
    ICurrentUserService CurrentUserService)
    : IRequestHandler<GetCustomerOrderQuery, OrderResponse>
{
    private readonly IOrderRepository _orderRepository = OrderRepository;
    private readonly ICurrentUserService _currentUserService = CurrentUserService;

    public async Task<OrderResponse> Handle(
        GetCustomerOrderQuery request,
        CancellationToken cancellationToken)
    {
        var customerId = _currentUserService.UserId;

        var order = await _orderRepository.GetByIdAsync(request.OrderId) ?? throw new KeyNotFoundException("Order was not found.");
        if (order.CustomerId != customerId)
        {
            throw new KeyNotFoundException(
                "Fuck off 🖕🏼 You are not authorized to access this order."
            );
        }

        return MapToResponse(order);
    }

    private static OrderResponse MapToResponse(Order order)
    {
        var items = order.Items
            .Select(item => new OrderItemResponse(
                item.FoodItemId,
                item.FoodItemName,
                item.UnitPrice,
                item.Quantity,
                item.Subtotal
            ))
            .ToList();

        return new OrderResponse(
            order.Id,
            order.OrderType,
            order.OrderStatus,
            order.PaymentStatus,
            order.DeliveryAddress,
            items,
            order.ItemsSubtotal,
            order.DeliveryFee,
            order.TotalAmount,
            order.CreatedAt,
            order.UpdatedAt,
            order.PaymentExpiresAt
        );
    }
}