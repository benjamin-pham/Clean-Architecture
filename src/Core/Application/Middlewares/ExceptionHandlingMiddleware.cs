using Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Application.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            HttpResponse response = context.Response;

            #region response
            response.StatusCode = (int)GetStatusCode(error);

            response.ContentType = "application/json";

            object resultObj = new
            {
                isSuccess = false,
                message = error.Message
            };

            string result = JsonSerializer.Serialize(resultObj);
            #endregion

            #region log
            if (response.StatusCode == (int)HttpStatusCode.InternalServerError)
            {
                //string body =
                _logger.LogCritical(@"Request: {Request} Status Code: {StatusCode} Message: {Message}",
                    context.Request.Path, response.StatusCode, error.Message);
            }
            #endregion

            await response.WriteAsync(result);
        }
    }

    private static HttpStatusCode GetStatusCode(Exception error) => error switch
    {
        AppException => HttpStatusCode.BadRequest,
        UnauthorizedException => HttpStatusCode.Unauthorized,
        ForbiddenException => HttpStatusCode.Forbidden,
        TimeoutException => HttpStatusCode.RequestTimeout,
        ValidationException => HttpStatusCode.UnprocessableContent,
        _ => HttpStatusCode.InternalServerError,
    };


}

public static class RequestExtensions
{
    public static async Task<string> ReadAsStringAsync(this Stream requestBody, bool leaveOpen = false)
    {
        using StreamReader reader = new(requestBody, leaveOpen: leaveOpen);
        var bodyAsString = await reader.ReadToEndAsync();
        return bodyAsString;
    }
}