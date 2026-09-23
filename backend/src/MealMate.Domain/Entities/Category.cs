using MealMate.Domain.Common;

namespace MealMate.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    public Category(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException( "Category name is required.", nameof(name));
        Id = Guid.NewGuid();
        Name = name.Trim();
        Description = description?.Trim();
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string name,string? description)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException( "Category name is required.", nameof(name));
        Name = name.Trim();
        Description = description?.Trim();
        UpdatedAt = DateTime.UtcNow;
    }
}