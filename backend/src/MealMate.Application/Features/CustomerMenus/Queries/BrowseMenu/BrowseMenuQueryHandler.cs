using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.CustomerMenus;
using MediatR;

namespace MealMate.Application.Features.CustomerMenus.Queries.BrowseMenu;

public sealed record BrowseMenuQueryHandler(
    IFoodItemRepository FoodItemRepository)
    : IRequestHandler<BrowseMenuQuery, List<CustomerMenuItemResponse>>
{
    private readonly IFoodItemRepository _foodItemRepository =
        FoodItemRepository;

    public async Task<List<CustomerMenuItemResponse>> Handle(
        BrowseMenuQuery request,
        CancellationToken cancellationToken)
    {
        var foodItems =
            await _foodItemRepository.GetAvailableAsync(request.CategoryId);

        return foodItems.Select(foodItem => new CustomerMenuItemResponse(
                foodItem.Id,
                foodItem.Name,
                foodItem.Description,
                foodItem.Price,
                foodItem.Image,
                foodItem.Category.Name
            )).ToList();
    }
}