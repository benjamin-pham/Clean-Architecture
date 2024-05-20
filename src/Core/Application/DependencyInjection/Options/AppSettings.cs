namespace Application.DependencyInjection.Options;
public class AppSettings
{
    public string EnvironmentName { get; set; }
    public int H { get; set; }
    public JWT Jwt { get; set; }
}
public class JWT
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}
public class AccessToken
{
    public string SecretKey { get; set; }
    public double Expiration { get; set; }
}
public class RefreshToken
{
    public double Expiration { get; set; }
}