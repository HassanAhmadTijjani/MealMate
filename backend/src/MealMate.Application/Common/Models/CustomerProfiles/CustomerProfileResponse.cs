namespace MealMate.Application.Common.Models.CustomerProfiles;

public sealed record CustomerProfileResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber,
    string? Address
);