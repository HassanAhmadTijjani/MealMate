using MealMate.Application.Common.Interfaces;
using MediatR;

namespace MealMate.Application.Auth.Commands.RegisterUser;

public class RegisterUserCommandHandler(IIdentityService identityService) : IRequestHandler<RegisterUserCommand, (bool Succeeded, string[] Errors)>
{
    public async Task<(bool Succeeded, string[] Errors)> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = await identityService.CreateUserAsync(
            request.Email,
            request.Password);
        if (!result.Succeed) return result;
        var roleAdded = await identityService.AddToRoleAsync(   
            request.Email,
            "Customer");
        if (!roleAdded)
        {
            return (
                false,
                ["User was created, but the Customer role could not be assigned."]
            );
        }

        return (true, []);
    }
}