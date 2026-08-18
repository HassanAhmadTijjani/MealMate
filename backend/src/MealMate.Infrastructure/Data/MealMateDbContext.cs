using MealMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MealMate.Infrastructure.Data;

public class MealMateDbContext(DbContextOptions<MealMateDbContext> options) : DbContext(options)
{
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
}