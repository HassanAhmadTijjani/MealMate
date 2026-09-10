namespace MealMate.Application.Common.Models.FoodItems;

public sealed record CreateFoodItemRequest(
    string Name,
    string? Description,
    decimal Price,
    string? Image,
    Guid CategoryId);