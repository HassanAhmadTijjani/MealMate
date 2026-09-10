using Microsoft.AspNetCore.Identity;
namespace MealMate.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    // MealMate-specific-info
    public string? Address { get; private set; }
}