using Application.DependencyInjection.Options;
using Application.DTOs.Authentication;
using Application.Libraries;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shared;

public class JwtHandler : IJwtHandler
{
    private readonly AppSettings _appSettings;
    public JwtHandler(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }
    public string GenerateAccessToken(string userId)
    {
        var authClaims = new List<Claim>
        {
            new Claim (ClaimTypes.NameIdentifier, userId),
            new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt.AccessToken.SecretKey));

        var token = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddDays(_appSettings.Jwt.AccessToken.Expiration),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public ClaimsPrincipal ValidateJwtToken(string token)
    {
        // Retrieve the JWT secret from environment variables and encode it
        var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt.AccessToken.SecretKey));

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = authKey,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return claimsPrincipal;
        }
        catch (SecurityTokenExpiredException)
        {
            // Handle token expiration
            throw new ApplicationException("Token has expired.");
        }
    }
    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString();
    }

    public TokenResponse GenerateToken(string userId) => new()
    {
        AccessToken = this.GenerateAccessToken(userId),
        RefreshToken = this.GenerateRefreshToken(),
        RefreshTokenExpiration = DateTimeOffset.UtcNow.AddDays(_appSettings.Jwt.RefreshToken.Expiration),
    };
}
