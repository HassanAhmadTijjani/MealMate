using MealMate.Application.Common.Interfaces;
using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.Restaurants.Commands.CreateRestaurant;

public sealed class CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository)
    : IRequestHandler<CreateRestaurantCommand, Guid>
{
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
    public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = new Restaurant(request.Name, request.Address, request.PhoneNumber, request.Email, request.Description, request.Logo);
        await _restaurantRepository.AddAsync(restaurant);
        return restaurant.Id;
    }
}