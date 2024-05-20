using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Libraries;
public static class ApplicationHttpContext
{
    private static IHttpContextAccessor _contextAccessor;

    public static HttpContext HttpContext => _contextAccessor.HttpContext;

    public static void Configure(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public static TService GetService<TService>()
    {
        return HttpContext.RequestServices.GetService<TService>();
    }

    public static TService CreateNewServiceScope<TService>()
    {
        var serviceProvider = HttpContext.RequestServices.GetService<IServiceProvider>();
        var scope = serviceProvider.CreateScope();
        return scope.ServiceProvider.GetService<TService>();
    }
    /// <summary>
    /// lấy ip client
    /// </summary>
    /// <returns></returns>
    public static string GetClientIpAddrest()
    {
        return HttpContext.Request.Headers["X-Forwarded-For"].ToString();
    }
}
