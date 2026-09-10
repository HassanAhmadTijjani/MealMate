using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.FoodItems.Queries.GetFoodItemById;

public sealed record GetFoodItemByIdQuery(Guid Id)
    : IRequest<FoodItem?>;