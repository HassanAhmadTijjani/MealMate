using MediatR;

namespace MealMate.Application.Features.Restaurants.Commands.UpdateRestaurant;

public sealed record UpdateRestaurantCommand(
Guid Id,
string Name,
string Address,
string PhoneNumber,
string Email,
string? Description,
bool IsOpen,
string? Logo
) : IRequest<bool>;