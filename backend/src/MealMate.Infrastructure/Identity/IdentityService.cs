using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace MealMate.Infrastructure.Identity;

public class IdentityService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenService jwtTokenService) : IIdentityService
{
    // CREATE USER
    public async Task<(bool Succeed, string[] Errors)> CreateUserAsync(string email, string password)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email
        };
        var result = await userManager.CreateAsync(user, password);
        return (
            result.Succeeded,
            result.Errors.Select(error => error.Description).ToArray()
        );
    }

    // ADD ROLE
    public async Task<bool> AddToRoleAsync(string email, string role)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null) return false;
        var result = await userManager.AddToRoleAsync(user, role);
        return result.Succeeded;
    }

    // LOGIN USER
    public async Task<(bool Succeeded, string? Token, string[] Errors)> LoginAsync(
     string email,
     string password)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return (
                false,
                null,
                ["Invalid email or password."]
            );
        }

        var passwordValid = await userManager.CheckPasswordAsync(user, password);
        if (!passwordValid)
        {
            return (
                false,
                null,
                ["Invalid email or password."]
            );
        }

        var roles = await userManager.GetRolesAsync(user);

        var token = await jwtTokenService.GenerateTokenAsync(user.Id, user.Email!, roles);

        return (
            true,
            token,
            []
        );
    }

    // GET USER BY ID
    public async Task<UserIdentityModel?> GetUserByIdAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user is null) return null;
        return new UserIdentityModel(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.PhoneNumber,
            user.Address
        );
    }

    public async Task<bool> UpdateUserAsync(
        string userId,
        string firstName,
        string lastName,
        string? phoneNumber,
        string? address)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null) return false;

        user.FirstName = firstName;
        user.LastName = lastName;
        user.PhoneNumber = phoneNumber;
        user.Address = address;

        var result = await userManager.UpdateAsync(user);

        return result.Succeeded;
    }
}