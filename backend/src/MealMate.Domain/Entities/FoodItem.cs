using MealMate.Domain.Common;

namespace MealMate.Domain.Entities;

public class FoodItem : BaseEntity
{


    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public string? Image { get; private set; }
    public bool IsAvailable { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    public FoodItem(string name, string? description, decimal price, string? image, Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Food item name is required", nameof(name));

        if (price < 0) throw new ArgumentException("Food item price must not be negative", nameof(price));

        Id = Guid.NewGuid();
        Name = name.Trim();
        Description = description?.Trim();
        Price = price;
        Image = image;
        CategoryId = categoryId;
        IsAvailable = true;
        CreatedAt = DateTime.UtcNow;
    }

    // Updating states
    public void UpdateDetails(string name, string? description, decimal price, string? image, Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Food item name is required", nameof(name));

        if (price < 0) throw new ArgumentException("Food item price must not be negative", nameof(price));
        Name = name.Trim();
        Description = description?.Trim();
        Price = price;
        Image = image;
        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
    }

    // Availability of Prodcut
    public void SetAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
        UpdatedAt = DateTime.UtcNow;
    }
}