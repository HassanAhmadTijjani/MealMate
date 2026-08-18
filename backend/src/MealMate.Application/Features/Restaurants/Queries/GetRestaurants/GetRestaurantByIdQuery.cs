using MealMate.Application.Common.Models.Restaurants;
using MediatR;

namespace MealMate.Application.Features.Restaurants.Queries.GetRestaurants;

public sealed record GetRestaurantByIdQuery(Guid Id) : IRequest<RestaurantResponse?>;
