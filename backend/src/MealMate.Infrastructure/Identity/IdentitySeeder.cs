using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace MealMate.Infrastructure.Identity;
public static class IdentitySeeder 
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = ["Admin", "Staff", "Customer"];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    // Admin setup
    public static async Task BootstrapAdminAsync(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        var enabled = configuration.GetValue<bool>("BootstrapAdmin:Enabled");
        if (!enabled) return;

        var email = configuration["BootstrapAdmin:Email"];
        var password = configuration["BootstrapAdmin:Password"];

        if (string.IsNullOrWhiteSpace(email)) throw new InvalidOperationException("BootstrapAdmin:Email is missing.");
        if (string.IsNullOrWhiteSpace(password)) throw new InvalidOperationException("BootstrapAdmin:Password is missing.");

        var existingUser = await userManager.FindByEmailAsync(email);

        if (existingUser is not null)
        {
            var isAlreadyAdmin = await userManager.IsInRoleAsync(existingUser, "Admin");
            if (!isAlreadyAdmin) throw new InvalidOperationException($"The bootstrap email '{email}' already belongs to a user " + "who is not an Admin. No role was changed.");
            Console.WriteLine($"Bootstrap Admin '{email}' already exists.");
            return;
        }
        var admin = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };
         var createResult = await userManager.CreateAsync(
            admin,
            password);

        if (!createResult.Succeeded)
        {
            var errors = string.Join("; ",createResult.Errors.Select(error => error.Description));

            throw new InvalidOperationException($"Failed to create bootstrap Admin: {errors}");
        }

        var roleResult = await userManager.AddToRoleAsync( admin, "Admin");

        if (!roleResult.Succeeded)
        {
            await userManager.DeleteAsync(admin);

            var errors = string.Join("; ",roleResult.Errors.Select(error => error.Description));

            throw new InvalidOperationException(
                $"Admin user was created but the Admin role could not be assigned. " +
                $"The user was removed. Errors: {errors}");
        }

        Console.WriteLine( $"Bootstrap Admin '{email}' created successfully.");
    }
}