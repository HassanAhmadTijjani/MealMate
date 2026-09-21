using MealMate.Application.Features.Carts.Commands.AddToCart;
using MealMate.Application.Features.Carts.Commands.RemoveCartItem;
using MealMate.Application.Features.Carts.Commands.UpdateCartItem;
using MealMate.Application.Features.Carts.Queries.GetCart;
using MediatR;

namespace MealMate.Api.Endpoints.CustomerCart;

public static class CustomerCartEndpoints
{
    public static IEndpointRouteBuilder MapCustomerCartEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("/api/cart")
            .RequireAuthorization();

        group.MapGet("/", async (
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new GetCartQuery(),
                cancellationToken
            );

            if (result is null)
            {
                return Results.Ok(new
                {
                    items = Array.Empty<object>(),
                    total = 0m
                });
            }

            return Results.Ok(result);
        });

        group.MapPost("/items", async (
            AddToCartRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new AddToCartCommand(
                    request.FoodItemId,
                    request.Quantity
                ),
                cancellationToken
            );

            return Results.Ok(result);
        });

        group.MapPut("/items", async (
            UpdateCartItemRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new UpdateCartItemCommand(
                    request.FoodItemId,
                    request.Quantity
                ),
                cancellationToken
            );

            return Results.Ok(result);
        });

        group.MapDelete("/items/{foodItemId:guid}", async (
            Guid foodItemId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new RemoveCartItemCommand(foodItemId),
                cancellationToken
            );

            return Results.Ok(result);
        });

        return endpoints;
    }
}