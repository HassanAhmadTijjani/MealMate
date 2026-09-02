using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MealMate.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MealMate.Infrastructure.Identity;

public class JwtTokenService(IOptions<JwtOptions> options) : IJwtTokenService
{
    public Task<string> GenerateTokenAsync(string userId, string email, IEnumerable<string> roles)
    {
        var jwtOptions = options.Value;

        // CLAIM
        var claims = new List<Claim>{
            new( ClaimTypes.NameIdentifier, userId),
            new( ClaimTypes.Email, email)
        };

        foreach (var role in roles)
        {
            claims.Add(
                new Claim(
                    ClaimTypes.Role,
                    role
                )
            );
        }
        // SYMMETRICSECURITYKEY
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));

        // SIGNINCREDENTIALS
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // JWTSECURITYTOKEN
        var token = new JwtSecurityToken(
        issuer: jwtOptions.Issuer,
        audience: jwtOptions.Audience,
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(jwtOptions.ExpirationMinutes),
        signingCredentials: credentials
        );

        // JWTSECURITYTOKENHANDLER
        var tokeString = new JwtSecurityTokenHandler().WriteToken(token);

        return Task.FromResult(tokeString);
    }
}