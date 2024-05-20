namespace Application.DTOs.Authentication;
public class TokenResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTimeOffset RefreshTokenExpiration { get; set; }
}
