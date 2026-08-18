namespace MealMate.Application.Common.Models.Restaurants;

public sealed record CreateRestaurantRequest(string Name, string Address, string PhoneNumber, string Email, string? Description, string? Logo);