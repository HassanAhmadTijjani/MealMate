using MealMate.Application.Common.Models.Carts;
using MediatR;

namespace MealMate.Application.Features.Carts.Commands.RemoveCart;

public sealed record RemoveCartItemCommand(Guid FoodItemId) : IRequest<CartResponse>;