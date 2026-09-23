namespace MealMate.Domain.Entities;
public class CartItem
{
    public Guid Id { get; private set; }
    public Guid CartId { get; private set; }
    public Guid FoodItemId { get; private set; }
    public int Quantity { get; private set; }
    public Cart Cart { get; private set; } = null!;
    public FoodItem FoodItem { get; private set; } = null!;
    private CartItem(){}
    public CartItem(Guid foodItemId, int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
        Id = Guid.NewGuid();
        FoodItemId = foodItemId;
        Quantity = quantity;
    }

    public void ChangeQuantity(int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
        Quantity = quantity;
    }

    public void IncreaseQuantity(int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
        Quantity += quantity;
    }
}