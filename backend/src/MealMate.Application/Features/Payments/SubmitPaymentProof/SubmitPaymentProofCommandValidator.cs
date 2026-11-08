using FluentValidation;
using MealMate.Application.Features.Payments.Commands.SubmitPaymentProof;

namespace MealMate.Application.Features.Payments.SubmitPaymentProof;
public sealed class SubmitPaymentProofCommandValidator : AbstractValidator<SubmitPaymentProofCommand>
{
    private const long MaxFileSizeBytes = 5 * 1024 * 1024;
    private static readonly string[] AllowedExtensions = [".jpeg", ".jpg", ".png", ".pdf"];

    public SubmitPaymentProofCommandValidator()
    {
        RuleFor(command => command.OrderId).NotEmpty();
        RuleFor(command => command.FileStream).NotNull().Must(stream => stream.CanRead).WithMessage("The stream file must be readable.").Must(stream => stream.CanSeek ? stream.Length <= MaxFileSizeBytes : true).WithMessage("The receipt file must not exceed 5 MB.");
        RuleFor(command => command.FileName).NotEmpty().Must(HaveAllowedExtension).WithMessage("Only JPG, JPEG, PNG, and PDF receipts are allowed.");

        RuleFor(command => command.ContentType).NotEmpty().Must(type => type is "image/jpeg" or"image/png" or"application/pdf").WithMessage("Unsupported receipt content type.");
    }

    private static bool HaveAllowedExtension(string? fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName)) return false;

        var extension = Path.GetExtension(fileName);

        return AllowedExtensions.Contains( extension, StringComparer.OrdinalIgnoreCase);

    }
}