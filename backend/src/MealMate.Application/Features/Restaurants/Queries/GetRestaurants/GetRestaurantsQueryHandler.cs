using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.Restaurants;
using MediatR;

namespace MealMate.Application.Features.Restaurants.Queries.GetRestaurants;

public sealed class GetRestaurantsQueryHandler(
    IRestaurantRepository restaurantRepository)
        : IRequestHandler<GetRestaurantsQuery, IReadOnlyList<RestaurantResponse>>
{
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;

    public async Task<IReadOnlyList<RestaurantResponse>> Handle(
        GetRestaurantsQuery request,
        CancellationToken cancellationToken)
    {
        var restaurants = await _restaurantRepository.GetAllAsync();

        return [.. restaurants
            .Select(restaurant => new RestaurantResponse(
                restaurant.Id,
                restaurant.Name,
                restaurant.Address,
                restaurant.PhoneNumber,
                restaurant.Email,
                restaurant.Description,
                restaurant.IsOpen,
                restaurant.CreatedAt,
                restaurant.UpdatedAt,
                restaurant.Logo))];
    }
}