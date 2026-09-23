using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.CustomerMenus;
using MediatR;

namespace MealMate.Application.Features.CustomerMenus.Queries.GetMenuItemById;

public sealed record GetMenuItemByIdQueryHandler(IFoodItemRepository FoodItemRepository) : IRequestHandler<GetMenuItemByIdQuery, CustomerMenuItemResponse?>
{
    private  readonly IFoodItemRepository _foodItemRepository = FoodItemRepository;
    public async Task<CustomerMenuItemResponse?> Handle(GetMenuItemByIdQuery request, CancellationToken cancellationToken)
    {
        var foodItem = await _foodItemRepository.GetByIdAsync(request.Id);
        if (foodItem is null) return null;
        if (!foodItem.IsAvailable) return null;
        return new CustomerMenuItemResponse(
            foodItem.Id,
            foodItem.Name,
            foodItem.Description,
            foodItem.Price,
            foodItem.Image,
            foodItem.Category.Name
        );
    }
}