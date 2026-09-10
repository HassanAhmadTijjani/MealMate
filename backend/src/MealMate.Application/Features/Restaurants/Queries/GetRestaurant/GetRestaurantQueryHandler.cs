using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.Restaurants;
using MediatR;

namespace MealMate.Application.Features.Restaurants.Queries.GetRestaurant;

public sealed class GetRestaurantQueryHandler(
    IRestaurantRepository restaurantRepository)
    : IRequestHandler<GetRestaurantQuery, RestaurantResponse?>
{
    public async Task<RestaurantResponse?> Handle(
        GetRestaurantQuery request,
        CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.GetAsync();

        if (restaurant is null)
            return null;

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
            restaurant.Logo
        );
    }
}