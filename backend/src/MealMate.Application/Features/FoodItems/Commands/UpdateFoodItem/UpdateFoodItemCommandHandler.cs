using MealMate.Application.Common.Interfaces;
using MediatR;

namespace MealMate.Application.Features.FoodItems.Commands.UpdateFoodItem;

public sealed class UpdateFoodItemCommandHandler(
    IFoodItemRepository foodItemRepository, ICategoryRepository categoryRepository)
    : IRequestHandler<UpdateFoodItemCommand, bool>
{
    private readonly IFoodItemRepository _foodItemRepository = foodItemRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<bool> Handle(
        UpdateFoodItemCommand request,
        CancellationToken cancellationToken)
    {
        if (await _categoryRepository.GetByIdAsync(request.CategoryId) is null) throw new ArgumentException("Category not found.");
        var foodItem = await _foodItemRepository.GetByIdAsync(request.Id);

        if (foodItem is null)
            return false;

        foodItem.UpdateDetails(
            request.Name,
            request.Description,
            request.Price,
            request.Image,
            request.CategoryId);

        foodItem.SetAvailability(request.IsAvailable);

        await _foodItemRepository.UpdateAsync(foodItem);

        return true;
    }
}