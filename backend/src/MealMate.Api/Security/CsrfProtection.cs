namespace MealMate.Api.Security;

public static class CsrfProtection
{
    public static bool IsValid(HttpRequest request)
    {
        if (!request.Cookies.TryGetValue("csrfToken", out var cookieToken))
            return false;

        if (!request.Headers.TryGetValue("X-CSRF-TOKEN", out var headerToken))
            return false;

        return cookieToken == headerToken;
    }
}