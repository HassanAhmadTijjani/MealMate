using MealMate.Api.Security;
using MealMate.Application.Common.Models.Categories;
using MealMate.Application.Features.Categories.Commands.CreateCategory;
using MealMate.Application.Features.Categories.Commands.DeleteCategory;
using MealMate.Application.Features.Categories.Commands.UpdateCategory;
using MealMate.Application.Features.Categories.Queries.GetCategories;
using MealMate.Application.Features.Categories.Queries.GetCategoryById;
using MediatR;

namespace MealMate.Api.Endpoints.Category;
public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder MapCategoryEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/category");

        // Create
        group.MapPost("", async (
            CreateCategoryRequest request,
            ISender sender,
       CancellationToken cancellationToken) =>
        {
            var command = new CreateCategoryCommand(
                request.Name,
                request.Description
            );
            var categoryId = await sender.Send(command, cancellationToken);
            return Results.Created(
                $"/api/category/{categoryId}",
                new { id = categoryId }
            );
        }).RequireAuthorization().AddEndpointFilter<CsrfEndpointFilter>();

        // Get all
        group.MapGet("", async (
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCategoryQuery();
            var categories = await sender.Send(query, cancellationToken);
            return Results.Ok(categories);
        });

        // Get  by ID
        group.MapGet("/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCategoryByIdQuery(id);
            var category = await sender.Send(query, cancellationToken);
            if (category is null) return Results.NotFound();
            return Results.Ok(category);
        });
       

        // Update
        group.MapPut("/{id:guid}", async (
            Guid id,
            UpdateCategoryRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateCategoryCommand(
                id,
                request.Name,
                request.Description
            );
            var updated = await sender.Send(command, cancellationToken);
            if (!updated)
                return Results.NotFound();

            return Results.NoContent();
        }).RequireAuthorization().AddEndpointFilter<CsrfEndpointFilter>();

        // Delete
        group.MapDelete("/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var deleted = await sender.Send(new DeleteCategoryCommand(id), cancellationToken);
            if (!deleted) return Results.NotFound();
            return Results.NoContent();
        }).RequireAuthorization().AddEndpointFilter<CsrfEndpointFilter>();
        return endpoints;
    }
}