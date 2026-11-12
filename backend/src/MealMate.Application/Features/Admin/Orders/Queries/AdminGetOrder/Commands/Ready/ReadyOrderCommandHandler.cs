using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.Orders;
using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Commands.Ready;

public sealed record ReadyOrderCommandHandler(IOrderRepository OrderRepository) : IRequestHandler<ReadyOrderCommand, OrderResponse>
{
    private readonly IOrderRepository _orderRepository = OrderRepository;
    public async Task<OrderResponse> Handle(ReadyOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId) ?? throw new KeyNotFoundException("Order not found");
        order.MarkAsReady();
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