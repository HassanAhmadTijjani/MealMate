using MealMate.Domain.Common;

namespace MealMate.Domain.Entities;
public class PaymentSettings:BaseEntity
{
    public string BankName { get; private set; } = string.Empty;
    public string AccountName { get; private set; } = string.Empty;
    public string AccountNumber { get; private set; } = string.Empty;
    public string? PaymentInstructions { get; private set; }
    public bool IsEnabled { get; private set; }

    // EF Core uses this constructor when materializing an entity.
    private PaymentSettings()
    {
    }
    public PaymentSettings(string bankName, string accountNumber, string accountName, string? paymentInstructions)
    {
        UpdateDetails(bankName, accountNumber, accountName, paymentInstructions);
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        IsEnabled = true;
    }
    public void UpdateDetails(string bankName, string accountNumber, string accountName, string? paymentInstructions)
    {
        if (string.IsNullOrWhiteSpace(bankName)) throw new ArgumentException("Bank name is required.", nameof(bankName));
        if (string.IsNullOrWhiteSpace(accountNumber)) throw new ArgumentException("Account number is required.", nameof(accountNumber));
        if (string.IsNullOrWhiteSpace(accountName)) throw new ArgumentException("Account name is required.", nameof(accountName));

        var validateAccountNumber = accountNumber.Trim();
        if (validateAccountNumber.Length != 10 || !validateAccountNumber.All(char.IsAsciiDigit)) throw new ArgumentException("Account number must be exsctly 10 digits.", nameof(accountNumber));

        BankName = bankName.Trim();
        AccountName = accountName.Trim();
        AccountNumber = validateAccountNumber;
        PaymentInstructions = paymentInstructions?.Trim();

        UpdatedAt = DateTime.UtcNow;
    }
    public void SetEnabledStatus(bool isEnabled)
    {
        IsEnabled = isEnabled;
        UpdatedAt = DateTime.UtcNow;
    }

}