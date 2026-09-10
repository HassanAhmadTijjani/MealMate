using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.FoodItems.Queries.GetFoodItems;

public sealed record GetFoodItemsQuery : IRequest<List<FoodItem>>;