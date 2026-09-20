namespace MealMate.Application.Features.Carts.Commands.AddToCart;

public sealed record AddToCartRequest(Guid FoodItemId, int Quantity);