using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.Orders;
using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.Orders.Commands.Checkout;

public sealed record CheckoutCommandHandler(
    ICartRepository CartRepository,
    IOrderRepository OrderRepository,
    ICurrentUserService CurrentUserService)
    : IRequestHandler<CheckoutCommand, OrderResponse>
{
    private readonly ICartRepository _cartRepository = CartRepository;
    private readonly IOrderRepository _orderRepository = OrderRepository;
    private readonly ICurrentUserService _currentUserService = CurrentUserService;

    // Temporary delivery fee until Admin Settings is implemented.
    private const decimal TemporaryDeliveryFee = 500m;

    public async Task<OrderResponse> Handle(
        CheckoutCommand request,
        CancellationToken cancellationToken)
    {
        var customerId = _currentUserService.UserId;

        var cart = await _cartRepository.GetByCustomerIdAsync(customerId) ?? throw new KeyNotFoundException("Cart was not found.");

        if (cart.Items.Count == 0)
        {
            throw new InvalidOperationException(
                "Cannot checkout an empty cart."
            );
        }

        foreach (var cartItem in cart.Items)
        {
            if (!cartItem.FoodItem.IsAvailable)
            {
                throw new InvalidOperationException(
                    $"Food item '{cartItem.FoodItem.Name}' is no longer available."
                );
            }
        }

        var orderItems = cart.Items
            .Select(cartItem => new OrderItem(
                cartItem.FoodItem.Id,
                cartItem.FoodItem.Name,
                cartItem.FoodItem.Price,
                cartItem.Quantity
            ))
            .ToList();

        var deliveryFee = request.OrderType == Domain.Enums.OrderType.Delivery
            ? TemporaryDeliveryFee
            : 0m;

        var order = new Order(
            customerId,
            request.OrderType,
            request.DeliveryAddress,
            deliveryFee,
            orderItems
        );

        await _orderRepository.AddAsync(order);

        var foodItemIds = cart.Items
            .Select(cartItem => cartItem.FoodItemId)
            .ToList();

        foreach (var foodItemId in foodItemIds)
        {
            cart.RemoveItem(foodItemId);
        }

        await _cartRepository.UpdateAsync(cart);

        return MapToResponse(order);
    }

    private static OrderResponse MapToResponse(
        Order order)
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