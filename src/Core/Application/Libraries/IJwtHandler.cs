using Application.DTOs.Authentication;
using System.Security.Claims;

namespace Application.Libraries;
public interface IJwtHandler
{
    string GenerateAccessToken(string userId);
    ClaimsPrincipal ValidateJwtToken(string token);
    string GenerateRefreshToken();
    TokenResponse GenerateToken(string userId);
}
