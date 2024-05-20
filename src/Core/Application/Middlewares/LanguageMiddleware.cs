using Microsoft.AspNetCore.Http;

namespace Application.Middlewares;
public class LanguageMiddleware
{
    private readonly RequestDelegate _next;

    public LanguageMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var language = context.Request.Headers["Language"].FirstOrDefault()?.Split(" ").Last();
        context.Items["Language"] = language;
        await _next(context);
    }
}
