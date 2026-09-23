using MealMate.Application.Common.Interfaces;
using MediatR;

namespace MealMate.Application.Auth.Commands.LoginUser;

public class LoginUserCommandHandler(IIdentityService identityService) : IRequestHandler<LoginUserCommand, (bool Succeeded, string? Token, string[] Errors)>
{
    public async Task<(bool Succeeded, string? Token, string[] Errors)> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await identityService.LoginAsync(
            request.Email,
            request.Password
        );
    }
}