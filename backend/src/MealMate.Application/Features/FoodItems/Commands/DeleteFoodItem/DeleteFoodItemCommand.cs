using MediatR;

namespace MealMate.Application.Features.FoodItems.Commands.DeleteFoodItem;

public sealed record DeleteFoodItemCommand(Guid Id) : IRequest<bool>;