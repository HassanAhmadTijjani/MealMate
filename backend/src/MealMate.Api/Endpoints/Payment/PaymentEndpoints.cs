using MealMate.Application.Features.Payments;
using MealMate.Application.Features.Payments.Commands.SubmitPaymentProof;
using MediatR;

namespace MealMate.Api.Endpoints.Payment;

public static class PaymentEndpoints
{
    public static IEndpointRouteBuilder MapPaymentEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/payments").WithTags("Payments").RequireAuthorization();

        group.MapPost(
            "/orders/{orderId:guid}/proof",
            SubmitPaymentProofAsync)
            .Accepts<SubmitPaymentProofRequest>("multipart/form-data")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .DisableAntiforgery();

        return app;
    }

    private static async Task<IResult> SubmitPaymentProofAsync(
        Guid orderId,
        IFormFile file,
        ISender sender,
        CancellationToken cancellationToken)
    {
        const long maxFileSize = 5 * 1024 * 1024;

        if (file.Length == 0)
            return Results.BadRequest("The receipt file is empty.");

        if (file.Length > maxFileSize)
            return Results.BadRequest("The receipt must not exceed 5 MB.");

        var extension = Path.GetExtension(file.FileName);

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };

        if (!allowedExtensions.Contains(
                extension,
                StringComparer.OrdinalIgnoreCase))
        {
            return Results.BadRequest(
                "Only JPG, JPEG, PNG, and PDF receipts are allowed.");
        }

        var allowedContentTypes = new[]
        {
            "image/jpeg",
            "image/png",
            "application/pdf"
        };

        if (!allowedContentTypes.Contains(
                file.ContentType,
                StringComparer.OrdinalIgnoreCase))
        {
            return Results.BadRequest("Unsupported receipt content type.");
        }

        await using var stream = file.OpenReadStream();

        var command = new SubmitPaymentProofCommand(
            orderId,
            stream,
            Path.GetFileName(file.FileName),
            file.ContentType);

        var submissionId = await sender.Send(command, cancellationToken);

        return Results.Ok(new
        {
            Message = "Payment proof submitted successfully.",
            SubmissionId = submissionId
        });
    }
}