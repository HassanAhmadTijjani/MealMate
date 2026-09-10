using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.Categories.Queries.GetCategories;

public sealed record GetCategoryQuery : IRequest<List<Category>>;