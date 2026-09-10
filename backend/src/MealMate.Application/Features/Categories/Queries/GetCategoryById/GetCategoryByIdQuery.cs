using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.Categories.Queries.GetCategoryById;

public sealed record GetCategoryByIdQuery(Guid Id) : IRequest<Category?>;