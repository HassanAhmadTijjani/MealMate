// knows how to retrieve that user's data

using MealMate.Application.Common.Models.Identity;


namespace MealMate.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<(bool Succeed, string[] Errors)> CreateUserAsync(string email,string password);

    Task<bool> AddToRoleAsync(string email, string role);

    Task<(bool Succeeded, string? Token, string[] Errors)> LoginAsync(string email,string password);

    Task<UserIdentityModel?> GetUserByIdAsync(string userId);
    Task<bool> UpdateUserAsync(string userId,string firstName,string lastName, string? phoneNumber, string? address);
}