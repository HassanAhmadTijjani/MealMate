namespace MealMate.Application.Common.Models.CustomerMenus;

public sealed record CustomerMenuItemResponse(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    string? Image,
    string Category
);