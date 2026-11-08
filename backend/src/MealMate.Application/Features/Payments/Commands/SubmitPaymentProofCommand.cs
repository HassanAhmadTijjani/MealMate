using MediatR;

namespace MealMate.Application.Features.Payments.Commands.SubmitPaymentProof;

public sealed record SubmitPaymentProofCommand(
    Guid OrderId,
    Stream FileStream,
    string FileName,
    string ContentType
) : IRequest<Guid>;