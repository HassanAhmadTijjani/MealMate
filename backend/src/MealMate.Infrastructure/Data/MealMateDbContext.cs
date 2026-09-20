using MealMate.Domain.Entities;
using MealMate.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MealMate.Infrastructure.Data;

public class MealMateDbContext(DbContextOptions<MealMateDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<FoodItem> FoodItems => Set<FoodItem>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Cart> Carts => Set<Cart>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FoodItem>().HasOne(foodItem => foodItem.Category).WithMany().HasForeignKey(foodItem => foodItem.CategoryId).OnDelete(DeleteBehavior.Restrict);
    }
}