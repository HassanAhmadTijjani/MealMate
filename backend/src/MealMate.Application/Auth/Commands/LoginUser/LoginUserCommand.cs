using MediatR;

namespace MealMate.Application.Auth.Commands.LoginUser;

public record LoginUserCommand(string Email, string Password) : IRequest<(bool Succeeded, string? Token, string[] Errors)>;