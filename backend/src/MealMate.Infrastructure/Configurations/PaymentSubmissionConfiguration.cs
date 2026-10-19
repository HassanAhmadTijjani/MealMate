using MealMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealMate.Infrastructure.Configurations;

public class PaymentSubmissionConfiguration
    : IEntityTypeConfiguration<PaymentSubmission>
{
    public void Configure(EntityTypeBuilder<PaymentSubmission> builder)
    {
        builder.HasKey(submission => submission.Id);

        builder.Property(submission => submission.OrderId)
            .IsRequired();

        builder.Property(submission => submission.ReceiptStorageKey)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(submission => submission.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(submission => submission.RejectionReason)
            .HasMaxLength(500);

        builder.Property(submission => submission.SubmittedAt)
            .IsRequired();

        builder.Property(submission => submission.ReviewedAt);

        builder.Property(submission => submission.ReviewedByAdminId)
            .HasMaxLength(450);

        builder.HasOne(submission => submission.Order)
            .WithMany(order => order.PaymentSubmissions)
            .HasForeignKey(submission => submission.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(submission => submission.OrderId)
            .HasDatabaseName("IX_PaymentSubmissions_OrderId");

        builder.HasIndex(submission => new
        {
            submission.OrderId,
            submission.SubmittedAt
        })
            .HasDatabaseName("IX_PaymentSubmissions_OrderId_SubmittedAt");
    }
}