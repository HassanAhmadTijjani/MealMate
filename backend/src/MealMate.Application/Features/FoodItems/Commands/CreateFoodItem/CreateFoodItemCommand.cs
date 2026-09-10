using MediatR;

namespace MealMate.Application.Features.FoodItems.Commands.CreateFoodItem;

public sealed record CreateFoodItemCommand(
    string Name,
    string? Description,
    decimal Price,
    string? Image,
    Guid CategoryId
) : IRequest<Guid>;