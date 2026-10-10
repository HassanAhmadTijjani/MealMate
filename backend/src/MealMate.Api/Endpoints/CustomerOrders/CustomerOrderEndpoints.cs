using MealMate.Application.Features.Orders.Commands.CancelOrder;
using MealMate.Application.Features.Orders.Commands.Checkout;
using MealMate.Application.Features.Orders.Queries.GetCustomerOrder;
using MealMate.Application.Features.Orders.Queries.GetCustomerOrders;
using MediatR;

namespace MealMate.Api.Endpoints.CustomerOrders;
public static class CustomerOrderEndpoints
{
    public static IEndpointRouteBuilder MapCustomerOrderEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/orders").RequireAuthorization();
        // Checkout
        group.MapPost("/checkout", async (
            ISender sender, CheckoutRequest request, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new CheckoutCommand(request.OrderType, request.DeliveryAddress), cancellationToken);
            return Results.Ok(result);
        });
        // Get all orders
        group.MapGet("/", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new GetCustomerOrdersQuery(),
                cancellationToken
            );
            return Results.Ok(result);
        });
        // Get single order
        group.MapGet("/{orderId:guid}", async (
            Guid orderId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetCustomerOrderQuery(orderId), cancellationToken);
            return Results.Ok(result);
        });
        // Cancel order
        group.MapPatch("/{orderId:guid}/cancel", async (
            Guid orderId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new CancelOrderCommand(orderId), cancellationToken);
            return Results.Ok(result);
        });
        return endpoints;
    }
}