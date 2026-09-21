using MealMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealMate.Infrastructure.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(item => item.Id);

        builder.Property(item => item.FoodItemId).IsRequired();

        builder.Property(item => item.Quantity).IsRequired();

        builder.HasOne(item => item.FoodItem).WithMany().HasForeignKey(item => item.FoodItemId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(item => new
        {
            item.CartId,
            item.FoodItemId
        }).IsUnique();
    }
}