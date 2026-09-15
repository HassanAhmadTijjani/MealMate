

using System.Security.Claims;
using MealMate.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace MealMate.Infrastructure.Identity;
public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService

{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public string? UserId => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
}
