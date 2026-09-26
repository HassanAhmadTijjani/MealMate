using MealMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealMate.Infrastructure.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(orderItem => orderItem.Id);

        builder.Property(orderItem => orderItem.OrderId)
            .IsRequired();

        builder.Property(orderItem => orderItem.FoodItemId)
            .IsRequired();

        builder.Property(orderItem => orderItem.FoodItemName)
            .IsRequired();

        builder.Property(orderItem => orderItem.UnitPrice)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(orderItem => orderItem.Quantity)
            .IsRequired();

        builder.Property(orderItem => orderItem.Subtotal)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.HasOne(orderItem => orderItem.Order)
            .WithMany(order => order.Items)
            .HasForeignKey(orderItem => orderItem.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<FoodItem>()
            .WithMany()
            .HasForeignKey(orderItem => orderItem.FoodItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}