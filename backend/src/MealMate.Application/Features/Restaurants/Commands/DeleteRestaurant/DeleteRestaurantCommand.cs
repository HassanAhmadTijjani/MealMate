using MediatR;

namespace MealMate.Application.Features.Restaurants.Commands.DeleteRestaurant;

public sealed record DeleteRestaurantCommand(Guid Id) : IRequest<bool>;