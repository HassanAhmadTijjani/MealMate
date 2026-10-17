using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.Orders;
using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.Orders.Commands.CancelOrder;

public sealed record CancelOrderCommandHandler(
IOrderRepository OrderRepository,
ICurrentUserService CurrentUserService) : IRequestHandler<CancelOrderCommand, OrderResponse>
{
    private readonly ICurrentUserService _currentUserService = CurrentUserService;
    private readonly IOrderRepository _orderRepository = OrderRepository;
    public async Task<OrderResponse> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var customerId = _currentUserService.UserId;
        var order = await _orderRepository.GetByIdAsync(request.OrderId);
        if (order is null || order.CustomerId != customerId)
        {
            throw new KeyNotFoundException("Order not found.");
        }
        order.Cancel();
        await _orderRepository.UpdateAsync(order);
        return MapToResponse(order);
    }
    private static OrderResponse MapToResponse(Order order)
    {
        var items = order.Items.Select(item => new OrderItemResponse(
            item.FoodItemId,
            item.FoodItemName,
            item.UnitPrice,
            item.Quantity,
            item.Subtotal
        )).ToList();
        return new OrderResponse(
            order.Id,
            order.OrderReference,
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