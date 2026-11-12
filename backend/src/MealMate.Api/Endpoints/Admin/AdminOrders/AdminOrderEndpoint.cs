using MealMate.Api.Security;
using MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Commands.Completed;
using MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Commands.Confirm;
using MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Commands.MarkAsFailed;
using MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Commands.MarkAsPaid;
using MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Commands.OutForDelivery;
using MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Commands.Preparing;
using MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Commands.Ready;
using MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Query;
using MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrders;
using MealMate.Domain.Enums;
using MediatR;

namespace MealMate.Api.Endpoints.Admin.AdminOrders;

public sealed record MarkOrderAsFailedRequest(string Reason);

public static class AdminOrderEndpoint
{
    public static IEndpointRouteBuilder MapAdminOrderEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/admin/orders").RequireAuthorization("AdminOnly").AddEndpointFilter<CsrfEndpointFilter>();

        // GET: all orders or filtered orders by status
        group.MapGet("", async (
            OrderStatus? status,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new GetAdminOrdersQuery(status),
                cancellationToken);

            return Results.Ok(result);
        });

        // GET single order
        group.MapGet("/{orderId:guid}", async (
            Guid orderId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new GetAdminOrderQuery(orderId),
                cancellationToken);

            return Results.Ok(result);
        });

        //  Updating Order statuses
        // CONFIRM
        group.MapPatch("/{orderId:guid}/confirm", async (
            Guid orderId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new ConfirmOrderCommand(orderId),
                cancellationToken);

            return Results.Ok(result);
        });

        // PREPARE
        group.MapPatch("/{orderId:guid}/prepare", async (
            Guid orderId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new PreparingOrderCommand(orderId),
                cancellationToken);

            return Results.Ok(result);
        });

        // READY
        group.MapPatch("/{orderId:guid}/ready", async (
            Guid orderId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new ReadyOrderCommand(orderId),
                cancellationToken);

            return Results.Ok(result);
        });

        // OUT-FOR-DELIVERY
        group.MapPatch("/{orderId:guid}/out-for-delivery", async (
            Guid orderId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new OutForDeliveryOrderCommand(orderId),
                cancellationToken);

            return Results.Ok(result);
        });

        // COMPLETE
        group.MapPatch("/{orderId:guid}/complete", async (
            Guid orderId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new CompleteOrderCommand(orderId),
                cancellationToken);

            return Results.Ok(result);
        });

        // MARK-AS-PAID
        group.MapPatch("/{orderId:guid}/paid", async (
            Guid orderId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new MarkOrderAsPaidCommand(orderId), cancellationToken);
            return Results.Ok(result);
        });

        // MARK-AS-FAILED
        group.MapPatch("/{orderId:guid}/payment-failed", async (
            Guid orderId,
            MarkOrderAsFailedRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new MarkOrderAsFailedCommand(orderId, request.Reason),
                cancellationToken);

            return Results.Ok(result);
        });

        return endpoints;
    }
}