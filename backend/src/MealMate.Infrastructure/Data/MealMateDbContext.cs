using MealMate.Domain.Entities;
using MealMate.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MealMate.Infrastructure.Data;

public class MealMateDbContext(DbContextOptions<MealMateDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
}