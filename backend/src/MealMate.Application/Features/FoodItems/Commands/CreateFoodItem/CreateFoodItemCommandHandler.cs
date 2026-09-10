using MealMate.Application.Common.Interfaces;
using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.FoodItems.Commands.CreateFoodItem;

public sealed class CreateFoodItemCommandHandler(
    IFoodItemRepository foodItemRepository, ICategoryRepository categoryRepository)
    : IRequestHandler<CreateFoodItemCommand, Guid>
{
    private readonly IFoodItemRepository _foodItemRepository = foodItemRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<Guid> Handle(
        CreateFoodItemCommand request,
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId) ?? throw new ArgumentException("Category not found.");
        
        var foodItem = new FoodItem(
            request.Name,
            request.Description,
            request.Price,
            request.Image,
            request.CategoryId);

        await _foodItemRepository.AddAsync(foodItem);

        return foodItem.Id;
    }
}