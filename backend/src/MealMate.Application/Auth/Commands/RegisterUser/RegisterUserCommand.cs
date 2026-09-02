using MediatR;

namespace MealMate.Application.Auth.Commands.RegisterUser;

public record RegisterUserCommand(string Email, string Password) : IRequest<(bool Succeeded, string[] Errors)>;