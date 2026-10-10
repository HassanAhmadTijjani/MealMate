namespace MealMate.Application.Common.Models.Orders;

public sealed record OrderItemResponse(
    Guid FoodItemId,
    string FoodItemName,
    decimal UnitPrice,
    int Quantity,
    decimal Subtotal
);