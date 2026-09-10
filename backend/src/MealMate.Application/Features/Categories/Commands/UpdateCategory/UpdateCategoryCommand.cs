using MediatR;

namespace MealMate.Application.Features.Categories.Commands.UpdateCategory;

public sealed record UpdateCategoryCommand(
    Guid Id,
    string Name,
string? Description
) : IRequest<bool>;