namespace MealMate.Application.Common.Interfaces;
public interface IIdentityService
{
    Task<(bool Succeed, string[] Errors)> CreateUserAsync(string email, string password);
    Task<bool> AddToRoleAsync(string email, string role);
    Task<(bool Succeeded, string? Token, string[] Errors)> LoginAsync(string email, string password);
}