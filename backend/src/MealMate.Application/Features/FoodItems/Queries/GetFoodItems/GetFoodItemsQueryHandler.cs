using MealMate.Application.Common.Interfaces;
using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.FoodItems.Queries.GetFoodItems;
public sealed record GetFoodItemsQueryHandler(IFoodItemRepository FoodItemRepository) : IRequestHandler<GetFoodItemsQuery, List<FoodItem>>
{
    private readonly IFoodItemRepository _foodItemRepository = FoodItemRepository;

    public async Task<List<FoodItem>> Handle(
        GetFoodItemsQuery request,
        CancellationToken cancellationToken)
    {
        return await _foodItemRepository.GetAllAsync();
    }
}