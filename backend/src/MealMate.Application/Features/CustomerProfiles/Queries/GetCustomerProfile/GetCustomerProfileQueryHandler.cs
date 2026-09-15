using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.CustomerProfiles;
using MediatR;

namespace MealMate.Application.Features.CustomerProfiles.Queries.GetCustomerProfile;

public sealed class GetCustomerProfileQueryHandler(
    ICurrentUserService currentUserService,
    IIdentityService identityService)
    : IRequestHandler<GetCustomerProfileQuery, CustomerProfileResponse?>
{
    public async Task<CustomerProfileResponse?> Handle(
        GetCustomerProfileQuery request,
        CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;

        if (userId is null)
        {
            return null;
        }

        var user = await identityService.GetUserByIdAsync(userId);

        if (user is null)
        {
            return null;
        }

        return new CustomerProfileResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email ?? string.Empty,
            user.PhoneNumber,
            user.Address
        );
    }
}