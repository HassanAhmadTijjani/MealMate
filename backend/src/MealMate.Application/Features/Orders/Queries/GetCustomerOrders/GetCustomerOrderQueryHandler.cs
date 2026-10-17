using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.Orders;
using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.Orders.Queries.GetCustomerOrders;

public sealed record GetCustomerOrderQueryHandler(
    ICartRepository CartRepository,
     IOrderRepository OrderRepository,
      ICurrentUserService CurrentUserService) : IRequestHandler<GetCustomerOrdersQuery, List<OrderResponse>>{
    private readonly IOrderRepository _orderRepository = OrderRepository;
    private readonly ICurrentUserService _currentUserService = CurrentUserService;

    public async Task<List<OrderResponse>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
    {
        var customerId = _currentUserService.UserId;
        var orders = await _orderRepository.GetByCustomerIdAsync(customerId);
        return [.. orders.Select(MapToResponse)];
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