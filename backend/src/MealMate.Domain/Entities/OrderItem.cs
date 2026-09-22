namespace MealMate.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid FoodItemId { get; private set; }

    // Snapshot fields
    // These values are frozen when the order is created.
    public string FoodItemName { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public decimal Subtotal { get; private set; }

    public Order Order { get; private set; } = null!;

    private OrderItem() { }

    public OrderItem(Guid foodItemId,string foodItemName,decimal unitPrice, int quantity)
    {
        if (foodItemId == Guid.Empty) throw new ArgumentException(   "Food item ID is required.",   nameof(foodItemId));

        if (string.IsNullOrWhiteSpace(foodItemName)) throw new ArgumentException(   "Food item name is required.",  nameof(foodItemName) );

        if (unitPrice < 0)throw new ArgumentException(  "Unit price cannot be negative.",  nameof(unitPrice) );

        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.",  nameof(quantity));

        Id = Guid.NewGuid();
        FoodItemId = foodItemId;
        FoodItemName = foodItemName;
        UnitPrice = unitPrice;
        Quantity = quantity;
        Subtotal = unitPrice * quantity;
    }
}