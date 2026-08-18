using MealMate.Application.Common.Models.Restaurants;
using MediatR;

namespace MealMate.Application.Features.Restaurants.Queries.GetRestaurants;

public sealed record GetRestaurantsQuery : IRequest<IReadOnlyList<RestaurantResponse>>;