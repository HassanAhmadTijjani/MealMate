using Microsoft.AspNetCore.Identity;

namespace MealMate.Infrastructure.Identity;
public static class IdentitySeeder 
{
    public static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = ["Admin", "Staff", "Customer"];
        foreach (var role in roles)
        {
            if(!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}