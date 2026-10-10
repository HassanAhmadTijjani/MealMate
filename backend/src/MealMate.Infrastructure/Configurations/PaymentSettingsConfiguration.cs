using System;
using MealMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealMate.Infrastructure.Configurations;

public class PaymentSettingsConfiguration : IEntityTypeConfiguration<PaymentSettings>
{
    public void Configure(EntityTypeBuilder<PaymentSettings> builder)
    {
        builder.HasKey(paymentSetting => paymentSetting.Id);
        builder.Property(paymentSetting => paymentSetting.Id).ValueGeneratedNever();
        builder.Property(paymentSetting => paymentSetting.BankName).IsRequired().HasMaxLength(100);
        builder.Property(paymentSetting => paymentSetting.AccountName).IsRequired().HasMaxLength(150);
        builder.Property(paymentSetting => paymentSetting.AccountNumber).IsRequired().HasMaxLength(10).IsFixedLength();
        builder.Property( paymentSetting => paymentSetting.PaymentInstructions).HasMaxLength(500);
        builder.Property(paymentSetting => paymentSetting.IsEnabled).IsRequired();
        builder.Property(paymentSetting => paymentSetting.CreatedAt).IsRequired();
        builder.Property(paymentSetting => paymentSetting.UpdatedAt);
    }
}
