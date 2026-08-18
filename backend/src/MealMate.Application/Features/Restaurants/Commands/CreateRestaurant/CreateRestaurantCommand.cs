using MediatR;

namespace MealMate.Application.Features.Restaurants.Commands.CreateRestaurant;

public sealed record CreateRestaurantCommand(
    string Name,
    string Address,
    string PhoneNumber,
    string Email,
    string? Description,
    string? Logo) : IRequest<Guid>;