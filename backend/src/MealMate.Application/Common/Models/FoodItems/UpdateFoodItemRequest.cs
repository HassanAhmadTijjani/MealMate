namespace MealMate.Application.Common.Models.FoodItems;

public sealed record UpdateFoodItemRequest(
    string Name,
    string? Description,
    decimal Price,
    string? Image,
    bool IsAvailable,
    Guid CategoryId);