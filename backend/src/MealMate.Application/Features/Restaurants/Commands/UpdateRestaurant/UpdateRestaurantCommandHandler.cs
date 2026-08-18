using MealMate.Application.Common.Interfaces;
using MealMate.Application.Features.Restaurants.Commands.UpdateRestaurant;
using MediatR;

namespace MealMAte.Application.Features.Restaurants.Commands.UpdateRestaurant;
public sealed record UpdateRestaurantCommandHandler(IRestaurantRepository RestaurantRepository) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    private readonly IRestaurantRepository _restaurantRepository = RestaurantRepository;
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);
        if (restaurant is null) return false;
        restaurant.UpdateDetails(
request.Name,
request.Address,
request.PhoneNumber,
request.Email,
request.Description,
request.IsOpen,
request.Logo
        );
        await _restaurantRepository.UpdateAsync(restaurant);
        return true;
    }
}