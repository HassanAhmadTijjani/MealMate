namespace MealMate.Application.Common.Models.Restaurants;

public sealed record RestaurantResponse(
    Guid Id,
    string Name,
    string Address,
    string PhoneNumber,
    string Email,
    string? Description,
    bool IsOpen,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? Logo);