using MealMate.Domain.Enums;

namespace MealMate.Domain.Entities;

public class PaymentSubmission
{
    public Guid Id { get; private set; }

    public Guid OrderId { get; private set; }

    public string ReceiptStorageKey { get; private set; } = string.Empty;

    public PaymentSubmissionStatus Status { get; private set; }

    public string? RejectionReason { get; private set; }

    public DateTime SubmittedAt { get; private set; }

    public DateTime? ReviewedAt { get; private set; }

    public string? ReviewedByAdminId { get; private set; }

    public Order Order { get; private set; } = null!;

    private PaymentSubmission() { }

    public PaymentSubmission(Guid orderId, string receiptStorageKey)
    {
        if (orderId == Guid.Empty)
        {
            throw new ArgumentException(
                "Order ID is required.",
                nameof(orderId));
        }

        if (string.IsNullOrWhiteSpace(receiptStorageKey))
        {
            throw new ArgumentException(
                "Receipt storage key is required.",
                nameof(receiptStorageKey));
        }

        Id = Guid.NewGuid();
        OrderId = orderId;
        ReceiptStorageKey = receiptStorageKey;
        Status = PaymentSubmissionStatus.AwaitingReview;
        SubmittedAt = DateTime.UtcNow;
    }
    public void Reject(string reason, string adminId)
    {
        if (Status != PaymentSubmissionStatus.AwaitingReview)
        {
            throw new InvalidOperationException(
                "Only submissions awaiting review can be rejected.");
        }

        if (string.IsNullOrWhiteSpace(reason))
        {
            throw new ArgumentException(
                "A rejection reason is required.",
                nameof(reason));
        }

        if (string.IsNullOrWhiteSpace(adminId))
        {
            throw new ArgumentException(
                "Admin ID is required.",
                nameof(adminId));
        }

        Status = PaymentSubmissionStatus.Rejected;
        RejectionReason = reason.Trim();
        ReviewedByAdminId = adminId;
        ReviewedAt = DateTime.UtcNow;
    }

    public void Verify(string adminId)
    {
        if (Status != PaymentSubmissionStatus.AwaitingReview)
        {
            throw new InvalidOperationException(
                "Only submissions awaiting review can be verified.");
        }

        if (string.IsNullOrWhiteSpace(adminId))
        {
            throw new ArgumentException(
                "Admin ID is required.",
                nameof(adminId));
        }

        Status = PaymentSubmissionStatus.Verified;
        RejectionReason = null;
        ReviewedByAdminId = adminId;
        ReviewedAt = DateTime.UtcNow;
    }
}