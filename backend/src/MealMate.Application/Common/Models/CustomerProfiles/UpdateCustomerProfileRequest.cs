namespace MealMate.Application.Common.Models.CustomerProfiles;

public sealed record UpdateCustomerProfileRequest(
string FirstName,
string LastName,
string? PhoneNumber,
string? Address
);