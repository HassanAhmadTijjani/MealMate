using MealMate.Application.Common.Interfaces;
using MediatR;

namespace MealMate.Application.Features.FoodItems.Commands.DeleteFoodItem;

public sealed record DeleteFoodItemCommandHandler(IFoodItemRepository FoodItemRepository) : IRequestHandler<DeleteFoodItemCommand, bool>
{
    private readonly IFoodItemRepository _foodItemRepository = FoodItemRepository;
    public async Task<bool> Handle(DeleteFoodItemCommand request, CancellationToken cancellationToken)
    {
        var foodItem = await _foodItemRepository.GetByIdAsync(request.Id);
        if (foodItem is null) return false;
        await _foodItemRepository.DeleteAsync(foodItem);
        
        return true;
    }
}