namespace MealMate.Application.Common.Models.Identity;

public sealed record UserIdentityModel(
    string Id,
    string FirstName,
    string LastName,
    string? Email,
    string? PhoneNumber,
    string? Address
);