using MealMate.Application.Common.Interfaces;
using MediatR;

namespace MealMate.Application.Features.CustomerProfiles.Commands.UpdateCustomerProfile;

public sealed class UpdateCustomerProfileCommandHandler(
    ICurrentUserService currentUserService,
    IIdentityService identityService)
    : IRequestHandler<UpdateCustomerProfileCommand, bool>
{
    public async Task<bool> Handle(
        UpdateCustomerProfileCommand request,
        CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;

        if (userId is null)
        {
            return false;
        }

        return await identityService.UpdateUserAsync(
            userId,
            request.FirstName,
            request.LastName,
            request.PhoneNumber,
            request.Address
        );
    }
}