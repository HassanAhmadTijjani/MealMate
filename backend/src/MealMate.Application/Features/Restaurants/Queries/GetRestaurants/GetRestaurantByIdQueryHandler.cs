using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.Restaurants;
using MealMate.Application.Features.Restaurants.Queries.GetRestaurants;
using MediatR;

namespace MealMate.Application.Features.Restaurants.Queries.GetRestaurants;

public sealed class GetRestaurantByIdQueryHandler(
    IRestaurantRepository restaurantRepository)
        : IRequestHandler<GetRestaurantByIdQuery, RestaurantResponse?>
{
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;

    public async Task<RestaurantResponse?> Handle(
        GetRestaurantByIdQuery request,
        CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(
            request.Id);

        if (restaurant is null)
        {
            return null;
        }

        return new RestaurantResponse(
            restaurant.Id,
            restaurant.Name,
            restaurant.Address,
            restaurant.PhoneNumber,
            restaurant.Email,
            restaurant.Description,
            restaurant.IsOpen,
            restaurant.CreatedAt,
            restaurant.UpdatedAt,
            restaurant.Logo);
    }
}