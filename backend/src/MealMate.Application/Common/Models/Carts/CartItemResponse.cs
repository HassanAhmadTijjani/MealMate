namespace MealMate.Application.Common.Models.Carts;

public sealed record CartItemResponse(
    Guid FoodItemID,
    string Name,
    decimal Price,
    string? Image,
    int Quantity,
    decimal Subtotal
);