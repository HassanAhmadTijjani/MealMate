namespace MealMate.Application.Common.Models.Restaurants;

public sealed record UpdateRestaurantRequest(
string Name,
string Address,
string PhoneNumber,
string Email,
string? Description,
bool IsOpen,
string? Logo
);