using MealMate.Application.Common.Interfaces;
using MealMate.Infrastructure.Data;
using MealMate.Infrastructure.Identity;
using MealMate.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MealMate.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MealMateDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddHttpContextAccessor();          //registers Microsoft's service that gives us access to the current HTTP request.
        services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<MealMateDbContext>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IFoodItemRepository, FoodItemRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();      //Whenever Application asks for ICurrentUserService, give it a CurrentUserService.
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        return services;
    }

}