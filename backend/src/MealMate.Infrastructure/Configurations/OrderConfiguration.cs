using MealMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealMate.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(order => order.Id);

        builder.Property(order => order.CustomerId)
            .IsRequired();

        builder.HasIndex(order => order.CustomerId);

        builder.Property(order => order.OrderType)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(order => order.OrderStatus)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(order => order.PaymentStatus)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(order => order.DeliveryAddress);

        builder.Property(order => order.ItemsSubtotal)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(order => order.DeliveryFee)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(order => order.TotalAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(order => order.CreatedAt)
            .IsRequired();

        builder.Property(order => order.UpdatedAt)
            .IsRequired();

        builder.Property(order => order.PaymentExpiresAt)
            .IsRequired();

        builder.HasMany(order => order.Items)
            .WithOne(item => item.Order)
            .HasForeignKey(item => item.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}