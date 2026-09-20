using MealMate.Application.Common.Models.Carts;
using MediatR;

namespace MealMate.Application.Features.Carts.Commands.AddToCart;

public sealed record AddToCartCommand(Guid FoodItemId, int Quantity): IRequest<CartResponse>;