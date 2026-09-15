using MealMate.Application.Common.Models.CustomerProfiles;
using MediatR;

namespace MealMate.Application.Features.CustomerProfiles.Queries.GetCustomerProfile;

public sealed record GetCustomerProfileQuery : IRequest<CustomerProfileResponse?>;