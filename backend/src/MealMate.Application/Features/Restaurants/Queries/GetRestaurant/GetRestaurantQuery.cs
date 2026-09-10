using MealMate.Application.Common.Models.Restaurants;
using MediatR;

namespace MealMate.Application.Features.Restaurants.Queries.GetRestaurant;

public sealed record GetRestaurantQuery : IRequest<RestaurantResponse?>;