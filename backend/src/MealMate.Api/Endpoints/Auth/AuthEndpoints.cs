using MealMate.Application.Auth.Commands.LoginUser;
using MealMate.Application.Auth.Commands.RegisterUser;
using MediatR;

namespace MealMate.Api.Endpoints.Auth;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth");

        group.MapPost("/register", async (
            RegisterUserCommand request,
            ISender sender) =>
        {
            var (Succeeded, Errors) = await sender.Send(request);

            if (!Succeeded)
            {
                return Results.BadRequest(new
                {
                    errors = Errors
                });
            }

            return Results.Ok(new
            {
                message = "Registration successful."
            });
        });

        group.MapPost("/login", async (
            LoginUserCommand request,
            ISender sender,
            HttpContext context) =>
        {
            var (Succeeded, Token, Errors) = await sender.Send(request);
            if (!Succeeded)
            {
                return Results.BadRequest(new
                {
                    error = Errors
                });
            }
            context.Response.Cookies.Append(
                "accessToken",
                Token!,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Path = "/"
                });
            return Results.Ok(new
            {
                message = "Login successful."
            });
        });
        group.MapPost("/logout", (HttpResponse response) =>
        {
            response.Cookies.Delete("accessToken");
            return Results.Ok(new
            {
                message= "Logout Successfully."
            });
        });
    }
}