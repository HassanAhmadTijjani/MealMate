namespace MealMate.Application.Features.Carts.Commands.UpdateCartItem;

public sealed record UpdateCartItemRequest(Guid FoodItemId, int Quantity);