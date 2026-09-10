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
    // public static bool IsValid(HttpRequest request)
    // {
    //     request.Cookies.TryGetValue("csrfToken", out var cookieToken);
    //     request.Headers.TryGetValue("X-CSRF-TOKEN", out var headerToken);

    //     Console.WriteLine($"CSRF Cookie: {cookieToken}");
    //     Console.WriteLine($"CSRF Header: {headerToken}");
    //     Console.WriteLine($"CSRF Match: {cookieToken == headerToken}");

    //     if (cookieToken is null)
    //         return false;

    //     if (headerToken.Count == 0)
    //         return false;

    //     return cookieToken == headerToken;
    // }
}