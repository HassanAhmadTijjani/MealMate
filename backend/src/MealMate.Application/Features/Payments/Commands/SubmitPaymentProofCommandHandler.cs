using MealMate.Application.Common.Interfaces;
using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.Payments.Commands.SubmitPaymentProof;

public sealed class SubmitPaymentProofCommandHandler(IOrderRepository orderRepository, IReceiptStorageService receiptStorageService, ICurrentUserService currentUserService) : IRequestHandler<SubmitPaymentProofCommand, Guid>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IReceiptStorageService _receiptStorageService = receiptStorageService;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<Guid> Handle(SubmitPaymentProofCommand request, CancellationToken cancellationToken)
    {
        var customerId = _currentUserService.UserId;

        if (string.IsNullOrWhiteSpace(customerId)) throw new UnauthorizedAccessException("You must be signed in to submit payment proof.");

        var order = await _orderRepository.GetByIdWithPaymentSubmissionsAsync(request.OrderId) ?? throw new KeyNotFoundException("Order was not found.");

        if (order.CustomerId != customerId) throw new UnauthorizedAccessException("You cannot submit payment proof for this order.");

        var submittedAt = DateTime.UtcNow;

        var isFirstSubmission = !order.FirstPaymentSubmissionAt.HasValue;

        // Validate the domain rules before uploading the file.
        if (isFirstSubmission)
        {
            order.RecordFirstPaymentSubmission(submittedAt);
        }
        else
        {
            order.RecordCorrectedPaymentSubmission(submittedAt);
        }

        var storageKey = await _receiptStorageService.UploadAsync(
            request.FileStream,
            request.FileName,
            request.ContentType,
            cancellationToken
        );

        var submission = new PaymentSubmission(
            order.Id,
            storageKey,
            submittedAt
        );

        try
        {
            await _orderRepository.SavePaymentSubmissionAsync(order, submission);
        }
        catch
        {
            // Prevent an orphaned receipt if database persistence fails.
            try
            {
                await _receiptStorageService.DeleteAsync(
                    storageKey,
                    cancellationToken);
            }
            catch
            {
                // Preserve the original database exception.
                // A production implementation should log this cleanup failure.
            }

            throw;
        }

        return submission.Id;
    }
}