using MediatR;

namespace MealMate.Application.Features.CustomerProfiles.Commands.UpdateCustomerProfile;

public sealed record UpdateCustomerProfileCommand(
string FirstName,
string LastName,
string? PhoneNumber,
string? Address
) : IRequest<bool>;