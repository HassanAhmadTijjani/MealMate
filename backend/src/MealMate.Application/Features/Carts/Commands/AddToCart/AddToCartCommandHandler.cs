using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.Carts;
using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.Carts.Commands.AddToCart;

public sealed record AddToCartCommandHandler(
    ICartRepository CartRepository,
    ICurrentUserService CurrentUserService,
    IFoodItemRepository FoodItemRepository) : IRequestHandler<AddToCartCommand, CartResponse>
{
    private readonly ICartRepository _cartRepository = CartRepository;
    private readonly ICurrentUserService _currentUserService = CurrentUserService;
    private readonly IFoodItemRepository _foodItemRepository = FoodItemRepository;
    public async Task<CartResponse> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        // Get UserID
        var userId = _currentUserService.UserId;
        // Get FoodItem By Id
        var foodItem = await _foodItemRepository.GetByIdAsync(request.FoodItemId) ?? throw new KeyNotFoundException("FoodItem was not found.");
        if (!foodItem.IsAvailable) throw new InvalidOperationException("FoodItem is not available yet.");
        // Get Customers Cart if none then Add one
        var cart = await _cartRepository.GetByCustomerIdAsync(userId);
        if (cart is null)
        {
            cart = new Cart(userId);
            cart.AddItem(foodItem.Id, request.Quantity);
            await _cartRepository.AddAsync(cart);
        }
        else
        {
            cart.AddItem(foodItem.Id, request.Quantity);
            await _cartRepository.UpdateAsync(cart);
        }
        cart = await _cartRepository.GetByCustomerIdAsync(userId) ?? throw new KeyNotFoundException("Cart was not found after saving.");
        // Get CartItems and make a list 
        var items = cart.Items.Select(item => new CartItemResponse(
            item.FoodItem.Id,
            item.FoodItem.Name,
            item.FoodItem.Price,
            item.FoodItem.Image,
            item.Quantity,
            item.FoodItem.Price * item.Quantity
        )).ToList();
        var total = items.Sum(item => item.Subtotal);
        return new CartResponse(
            cart.Id,
            cart.CreatedAt,
            cart.UpdatedAt,
            items,
            total
        );
    }
}