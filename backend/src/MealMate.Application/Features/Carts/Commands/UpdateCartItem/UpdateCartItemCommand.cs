using MealMate.Application.Common.Models.Carts;
using MediatR;

namespace MealMate.Application.Features.Carts.Commands.UpdateCartItem;

public sealed record UpdateCartItemCommand(
    Guid FoodItemId,
    int Quantity
) : IRequest<CartResponse>;