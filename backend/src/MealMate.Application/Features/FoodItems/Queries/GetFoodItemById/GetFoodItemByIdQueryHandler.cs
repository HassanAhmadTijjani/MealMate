using MealMate.Application.Common.Interfaces;
using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.FoodItems.Queries.GetFoodItemById;

public sealed class GetFoodItemByIdQueryHandler(
    IFoodItemRepository foodItemRepository)
    : IRequestHandler<GetFoodItemByIdQuery, FoodItem?>
{
    private readonly IFoodItemRepository _foodItemRepository = foodItemRepository;

    public async Task<FoodItem?> Handle(
        GetFoodItemByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _foodItemRepository.GetByIdAsync(request.Id);
    }
}