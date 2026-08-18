using MealMate.Api.Endpoints.Restaurants;
using MealMate.Application;
using MealMate.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Cross Origin Resource Sharing(CORS)
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// Register Application DependencyInjection
builder.Services.AddApplication();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// EndPoints
app.MapRestaurantEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("Frontend");
app.UseHttpsRedirection();



app.Run();


