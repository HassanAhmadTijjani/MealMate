using MealMate.Api.Security;
using MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder;
using MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrders;
using MediatR;

namespace MealMate.Api.Endpoints.Admin.AdminOrders;
public static class AdminOrderEndpoint
{
    public static IEndpointRouteBuilder MapAdminOrderEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/admin/orders").RequireAuthorization("AdminOnly").AddEndpointFilter<CsrfEndpointFilter>();

        // GET: all orders
        group.MapGet("", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetAdminOrdersQuery(), cancellationToken);
            return Results.Ok(result);
        });

        // GET single order
        group.MapGet("/{orderId:guid}", async (
            Guid orderId,
            ISender sender,
            CancellationToken cancellationToken
        ) =>
{
    var result = await sender.Send(
        new GetAdminOrderQuery(orderId),
        cancellationToken);

    return Results.Ok(result);
});
        return endpoints;
    }
}