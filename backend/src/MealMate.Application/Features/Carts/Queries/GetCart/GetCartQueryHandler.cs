using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.Carts;
using MediatR;

namespace MealMate.Application.Features.Carts.Queries.GetCart;

public sealed record GetCartQueryHandler(ICartRepository CartRepository, ICurrentUserService CurrentUserService) : IRequestHandler<GetCartQuery, CartResponse?>
{
    private readonly ICartRepository _cartRepository = CartRepository;
    private readonly ICurrentUserService _currentUserService = CurrentUserService;
    public async Task<CartResponse> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var cart = await _cartRepository.GetByCustomerIdAsync(userId);
        if(cart is null) return null;
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