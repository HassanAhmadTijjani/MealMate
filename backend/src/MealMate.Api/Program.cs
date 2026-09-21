using MealMate.Api.Endpoints.Auth;
using MealMate.Api.Endpoints.Restaurant;
using MealMate.Application;
using MealMate.Infrastructure;
using MealMate.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MealMate.Api.Endpoints.FoodItem;
using MealMate.Api.Endpoints.Category;
using MealMate.Api.Endpoints.Profiles;
using MealMate.Api.Endpoints.CustomerMenus;
using MealMate.Api.Endpoints.CustomerCart;


var builder = WebApplication.CreateBuilder(args);

// Cross Origin Resource Sharing(CORS)
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Add Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);



// Register Application DependencyInjection
builder.Services.AddApplication();

// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>()!;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
    };
    // Read cookiew
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["accessToken"];

            return Task.CompletedTask;
        }
    };
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();
var app = builder.Build();

// Role Seeder
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await IdentitySeeder.SeedRoleAsync(roleManager);
}

// EndPoints
app.MapRestaurantEndpoints();
app.MapAuthEndpoints();
app.MapFoodItemEndpoints();
app.MapCategoryEndpoints();
app.MapCustomerProfileEndpoints();
app.MapCustomerMenuEndpoints();
app.MapCustomerCartEndpoints();


// CSRF Token
app.MapGet("/api/auth/csrf-token", (HttpResponse response) =>
{
    var token = Convert.ToBase64String(
        System.Security.Cryptography.RandomNumberGenerator.GetBytes(32)
    );

    response.Cookies.Append("csrfToken", token, new CookieOptions
    {
        HttpOnly = true,
        Secure = false,         //Temporary for development
        SameSite = SameSiteMode.Lax,            //Temporary for development
        Path = "/"
    });

    return Results.Ok(new
    {
        token
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("Frontend");
app.UseAuthentication();
app.UseAuthorization();


app.Run();


