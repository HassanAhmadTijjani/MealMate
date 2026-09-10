using MealMate.Api.Security;
using MealMate.Application.Common.Models.FoodItems;
using MealMate.Application.Features.FoodItems.Commands.CreateFoodItem;
using MealMate.Application.Features.FoodItems.Commands.DeleteFoodItem;
using MealMate.Application.Features.FoodItems.Commands.UpdateFoodItem;
using MealMate.Application.Features.FoodItems.Queries.GetFoodItemById;
using MealMate.Application.Features.FoodItems.Queries.GetFoodItems;
using MediatR;

namespace MealMate.Api.Endpoints.FoodItem;
public static class FoodItemEndpoints
{
    public static IEndpointRouteBuilder MapFoodItemEndpoints(        
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/food");
        // Creeate
        group.MapPost("", async (
            CreateFoodItemRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
                {
                    var command = new CreateFoodItemCommand(
                        request.Name,
                        request.Description,
                        request.Price,
                        request.Image,
                        request.CategoryId);
                    var foodItemId = await sender.Send(command, cancellationToken);
                    return Results.Created(
                        $"/api/food/{foodItemId}",
                        new {id = foodItemId}
                    );
                }).RequireAuthorization().AddEndpointFilter<CsrfEndpointFilter>();

        // Get all
        group.MapGet("", async (
         ISender sender,
         CancellationToken cancellationToken) =>
        {
            var query = new GetFoodItemsQuery();
            var foodItems = await sender.Send(query, cancellationToken);
            // if (foodItems is null) return Results.NotFound();
            return Results.Ok(foodItems);
        });

        // GET BY ID
        group.MapGet("/{id:guid}", async (
          Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetFoodItemByIdQuery(id);
            var foodItem = await sender.Send(query, cancellationToken);
            if (foodItem is null) return Results.NotFound();
            return Results.Ok(foodItem);
        });

        // Update single fooditem
        group.MapPut("/{id:guid}", async (
            Guid id, UpdateFoodItemRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpdateFoodItemCommand(
                id,
                request.Name,
                request.Description,
                request.Price,
                request.Image,
                request.IsAvailable,
                request.CategoryId);

                var updated = await sender.Send(command, cancellationToken);

                if (!updated)
                    return Results.NotFound();

                return Results.NoContent();
        }).RequireAuthorization().AddEndpointFilter<CsrfEndpointFilter>();

        // Delete
        group.MapDelete("/{id:guid}", async (
            Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var deleted = await sender.Send(new DeleteFoodItemCommand(id), cancellationToken);
            if (!deleted) return Results.NotFound();
            return Results.NoContent();
        }).RequireAuthorization().AddEndpointFilter<CsrfEndpointFilter>();
        return endpoints;
    }
}