using MealMate.Application.Common.Models.CustomerProfiles;
using MealMate.Application.Features.CustomerProfiles.Commands.UpdateCustomerProfile;
using MealMate.Application.Features.CustomerProfiles.Queries.GetCustomerProfile;
using MediatR;

namespace MealMate.Api.Endpoints.Profiles;
public static class CustomerProfileEndpoints
{
    public static IEndpointRouteBuilder MapCustomerProfileEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/profile").RequireAuthorization();
        // GET
        group.MapGet("/", async (ISender sender) =>
        {
            var profile = await sender.Send(new GetCustomerProfileQuery());
            return profile is null ? Results.NotFound() : Results.Ok(profile);
        });

        // UPDATE
        group.MapPut("/", async (
           UpdateCustomerProfileRequest request,
           ISender sender) =>
       {
           var command = new UpdateCustomerProfileCommand(
               request.FirstName,
               request.LastName,
               request.PhoneNumber,
               request.Address
           );
           var updated = await sender.Send(command);
           return updated
               ? Results.NoContent()
               : Results.NotFound();
       });

        return endpoints;
    }
}