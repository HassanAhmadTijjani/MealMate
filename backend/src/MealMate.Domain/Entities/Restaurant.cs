using MealMate.Domain.Common;

namespace MealMate.Domain.Entities;

public class Restaurant : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsOpen { get; private set; }

    public Restaurant(
        string name,
        string address,
        string phoneNumber,
        string email,
        string? description,
        string? logo)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Restaurant name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Restaurant address is required.", nameof(address));

        if (string.IsNullOrWhiteSpace(phoneNumber)) throw new ArgumentException("Restaurant phone number is required.", nameof(phoneNumber));

        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Restaurant email is required.", nameof(email));

        Id = Guid.NewGuid();
        Name = name.Trim();
        Address = address.Trim();
        PhoneNumber = phoneNumber.Trim();
        Email = email.Trim();
        Description = description?.Trim();
        Logo = logo;
        IsOpen = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(
        string name,
        string address,
        string phoneNumber,
        string email,
        string? description,
        string? logo)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Restaurant name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Restaurant address is required.", nameof(address));

        if (string.IsNullOrWhiteSpace(phoneNumber)) throw new ArgumentException("Restaurant phone number is required.",  nameof(phoneNumber));

        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Restaurant email is required.", nameof(email));

        Name = name.Trim();
        Address = address.Trim();
        PhoneNumber = phoneNumber.Trim();
        Email = email.Trim();
        Description = description?.Trim();
        Logo = logo;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetOpenStatus(bool isOpen)
    {
        IsOpen = isOpen;
        UpdatedAt = DateTime.UtcNow;
    }
}