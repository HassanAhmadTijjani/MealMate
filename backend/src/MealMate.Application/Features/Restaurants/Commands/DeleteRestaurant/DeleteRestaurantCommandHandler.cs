using MealMate.Application.Common.Interfaces;
using MediatR;

namespace MealMate.Application.Features.Restaurants.Commands.DeleteRestaurant;
public sealed record DeleteRestaurantCommandHandler(IRestaurantRepository restaurantRepository) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);
        if (restaurant is null) return false;
        await _restaurantRepository.DeleteAsync(restaurant);
        return true;
    }
}