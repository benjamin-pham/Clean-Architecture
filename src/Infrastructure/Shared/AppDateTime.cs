using Application.Libraries;

namespace Shared;

public class AppDateTime : IDateTime
{
    public AppDateTime()
    {
        this.Now = DateTimeOffset.UtcNow;
    }
    public DateTimeOffset Now { get; }
}