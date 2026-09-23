using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.Carts;
using MediatR;

namespace MealMate.Application.Features.Carts.Commands.UpdateCartItem;

public sealed record UpdateCartItemCommandHandler(ICartRepository CartRepository, ICurrentUserService CurrentUserService) : IRequestHandler<UpdateCartItemCommand, CartResponse>
{
    private readonly ICartRepository _cartRepository = CartRepository;
    private readonly ICurrentUserService _currentUserService = CurrentUserService;
    public async Task<CartResponse> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        // GET users ID
        var userId = _currentUserService.UserId;
        // GET users cart
        var cart = await _cartRepository.GetByCustomerIdAsync(userId) ?? throw new KeyNotFoundException("Cart was not found.");
        cart.ChangeItemQuantity(request.FoodItemId, request.Quantity);
        await _cartRepository.UpdateAsync(cart);
        // GET Cart Items
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