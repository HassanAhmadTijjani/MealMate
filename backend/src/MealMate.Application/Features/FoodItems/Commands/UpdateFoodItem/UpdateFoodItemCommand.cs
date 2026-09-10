using MediatR;

namespace MealMate.Application.Features.FoodItems.Commands.UpdateFoodItem;

public sealed record UpdateFoodItemCommand(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    string? Image,
    bool IsAvailable,
    Guid CategoryId
) : IRequest<bool>;