namespace MealMate.Application.Common.Models.Carts;

public sealed record CartResponse(
    Guid Id,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<CartItemResponse> Items,
    decimal Total
);