namespace MealMate.Domain.Entities;

public class Cart
{
    public Guid Id { get; private set; }

    public string CustomerId { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public ICollection<CartItem> Items { get; private set; } =
        [];

    private Cart()
    {
    }

    public Cart(string customerId)
    {
        if (string.IsNullOrWhiteSpace(customerId))throw new ArgumentException( "Customer ID is required.", nameof(customerId));
        Id = Guid.NewGuid();
        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddItem(Guid foodItemId, int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        var existingItem = Items.FirstOrDefault(
            item => item.FoodItemId == foodItemId
        );

        if (existingItem is not null)
        {
            existingItem.IncreaseQuantity(quantity);
            return;
        }
        Items.Add(new CartItem(foodItemId, quantity));
    }

    public void ChangeItemQuantity(Guid foodItemId, int quantity)
    {
        var item = Items.FirstOrDefault(item => item.FoodItemId == foodItemId) ?? throw new InvalidOperationException("Cart item was not found.");
        item.ChangeQuantity(quantity);
    }

    public void RemoveItem(Guid foodItemId)
    {
        var item = Items.FirstOrDefault(
            item => item.FoodItemId == foodItemId
        );
        if (item is null) return;
        Items.Remove(item);
    }
}