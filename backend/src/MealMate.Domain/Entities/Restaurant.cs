using MealMate.Domain.Common;

namespace MealMate.Domain.Entities;

public class Restaurant : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string? Description { get; private set; } = string.Empty;
    public bool IsOpen { get; private set; }

    public Restaurant(
    string name,
    string address,
    string phoneNumber,
    string email,
    string? description,
    string? logo)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Restaurant name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address is required.", nameof(address));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number is required.", nameof(phoneNumber));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.", nameof(email));

        Id = Guid.NewGuid();

        Name = name.Trim();
        Address = address.Trim();
        PhoneNumber = phoneNumber.Trim();
        Email = email.Trim();
        Description = description?.Trim();

        IsOpen = true;
        CreatedAt = DateTime.UtcNow;
        Logo = logo;
    }

    public void UpdateDetails(
        string name,
        string address,
        string phoneNumber,
        string email,
        string? description,
        bool isOpen,
        string? logo)
    {
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
        Description = description;
        IsOpen = isOpen;
        Logo = logo;

        UpdatedAt = DateTime.UtcNow;
    }
}