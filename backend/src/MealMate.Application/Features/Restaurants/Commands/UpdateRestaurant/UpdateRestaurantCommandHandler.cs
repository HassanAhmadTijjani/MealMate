using MealMate.Application.Common.Interfaces;
using MediatR;

namespace MealMate.Application.Features.Restaurants.Commands.UpdateRestaurant;

public sealed class UpdateRestaurantCommandHandler(
    IRestaurantRepository restaurantRepository)
    : IRequestHandler<UpdateRestaurantCommand, bool>
{
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;

    public async Task<bool> Handle(UpdateRestaurantCommand request,CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepository.GetAsync();

        if (restaurant is null) return false;

        restaurant.UpdateDetails(
            request.Name,
            request.Address,
            request.PhoneNumber,
            request.Email,
            request.Description,
            request.Logo
        );

        restaurant.SetOpenStatus(request.IsOpen);

        await _restaurantRepository.UpdateAsync(restaurant);

        return true;
    }
}