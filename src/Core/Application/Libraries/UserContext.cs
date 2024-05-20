using Domain.Entities;

namespace Application.Libraries;
public class UserContext
{
    public static User Instance { get => (User)ApplicationHttpContext.HttpContext.Items["UserContext"]; }
}