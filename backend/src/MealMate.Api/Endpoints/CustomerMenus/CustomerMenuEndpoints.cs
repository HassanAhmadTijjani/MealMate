using MealMate.Application.Features.CustomerMenus.Queries.BrowseMenu;
using MealMate.Application.Features.CustomerMenus.Queries.GetMenuItemById;
using MediatR;

namespace MealMate.Api.Endpoints.CustomerMenus;

public static class CustomerMenuEndpoints
{
    public static IEndpointRouteBuilder MapCustomerMenuEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/menu");
        group.MapGet("/", async (Guid? categoryId, ISender sender, CancellationToken cancellationToken) =>
        {
            var menu = await sender.Send(new BrowseMenuQuery(categoryId), cancellationToken);
            return Results.Ok(menu);
        });

        group.MapGet("/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var menuItem = await sender.Send(
                new GetMenuItemByIdQuery(id), cancellationToken);

            return menuItem is null ? Results.NotFound() : Results.Ok(menuItem);
        });
        return endpoints;
    }
}