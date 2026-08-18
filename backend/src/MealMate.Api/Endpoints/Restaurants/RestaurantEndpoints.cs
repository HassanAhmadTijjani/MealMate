using MealMate.Application.Common.Models.Restaurants;
using MealMate.Application.Features.Restaurants.Commands.CreateRestaurant;
using MealMate.Application.Features.Restaurants.Commands.DeleteRestaurant;
using MealMate.Application.Features.Restaurants.Commands.UpdateRestaurant;
using MealMate.Application.Features.Restaurants.Queries.GetRestaurants;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MealMate.Api.Endpoints.Restaurants;

public static class RestaurantEndpoints
{
    public static IEndpointRouteBuilder MapRestaurantEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/restaurants", async (
            CreateRestaurantRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateRestaurantCommand(
                request.Name,
                request.Address,
                request.PhoneNumber,
                request.Email,
                request.Description,
                request.Logo);

            var restaurantId = await sender.Send(
                command,
                cancellationToken);

            return Results.Created(     //201 Created
                $"/api/restaurants/{restaurantId}",
                new { id = restaurantId });
        });

        endpoints.MapGet("/api/restaurants", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetRestaurantsQuery();
            // MediatR finds GetRestaurantsQuery => GetRestaurantsQueryHandler
            var restaurants = await sender.Send(query, cancellationToken);
            return Results.Ok(restaurants);     //200 OK
        });

        // Get restaurant by ID
        endpoints.MapGet("/api/restaurants/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetRestaurantByIdQuery(id);
            var restaurant = await sender.Send(query, cancellationToken);
            if (restaurant is null) return Results.NotFound();     // Produces a StatusCodes.Status404NotFound response
            return Results.Ok(restaurant);
        });

        // Put changing a single restaurant
        endpoints.MapPut("/api/restaurants/{id:guid}", async (Guid id, UpdateRestaurantRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpdateRestaurantCommand(
id,
request.Name,
request.Address,
request.PhoneNumber,
request.Email,
request.Description,
request.IsOpen,
request.Logo
            );
            var updated = await sender.Send(command, cancellationToken);
            if (!updated) return Results.NotFound();
            return Results.NoContent();     //Produces a StatusCodes.Status204NoContent response.
        });

        // Delete
        endpoints.MapDelete("/api/restaurants/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var deleted = await sender.Send(new DeleteRestaurantCommand(id), cancellationToken);
            if(!deleted) return Results.NotFound();
            return Results.NoContent();
        });
        return endpoints;
    }
}