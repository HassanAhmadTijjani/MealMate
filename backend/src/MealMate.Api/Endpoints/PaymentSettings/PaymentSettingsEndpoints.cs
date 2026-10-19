using MealMate.Api.Security;
using MealMate.Application.Common.Models.PaymentSetting;
using MealMate.Application.Features.PaymentSetting.Commands;
using MealMate.Application.Features.PaymentSetting.Queries;
using MediatR;

namespace MealMate.Api.Endpoints.PaymentSettings;
public static class PaymentSettingsEndpoints
{
    public static IEndpointRouteBuilder MapPaymentSettingsEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("api/admin/payment-settings").RequireAuthorization("AdminOnly").AddEndpointFilter<CsrfEndpointFilter>();

        // GET settings
        group.MapGet("", async (ISender sender) =>
        {
            var result = await sender.Send(new PaymentSettingsQuery());
            return result is null ? Results.NoContent() : Results.Ok(result);
        });

        // UPSERT settings
        group.MapPut("", async (
            PaymentSettingsRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpsertPaymentSettingsCommand(
                request.BankName,
                request.AccountName,
                request.AccountNumber,
                request.PaymentInstructions
            );
            var result = await sender.Send(command, cancellationToken);
            return Results.Ok(result);
        });

        // TOGGLE settings
        group.MapPatch("/toggle", async (PaymentSettingsEnabledRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new SetPaymentSettingsEnabledCommand(request.IsEnabled), cancellationToken);
            return Results.Ok(result);
        });
        return endpoints;
    }
}