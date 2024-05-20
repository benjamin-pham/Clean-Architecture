using Application.Constants;
using Application.Exceptions;
using Application.Libraries;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Application.Middlewares;
public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context, IJwtHandler jwtHandler)
    {
        // Get the token from the Authorization header
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (!token.IsNullOrEmpty())
        {
            try
            {
                // Verify the token using the JwtSecurityTokenHandlerWrapper
                var claimsPrincipal = jwtHandler.ValidateJwtToken(token);

                // Extract the user ID from the token
                var userIdStr = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                bool isValid = Guid.TryParse(userIdStr, out Guid userId);
                if (isValid)
                {
                    // Store the user in the HttpContext items for later use
                    IUserRepository userRepository = ApplicationHttpContext.GetService<IUserRepository>();
                    User user = await userRepository.FindByIdAsync(userId);
                    context.Items["UserContext"] = user;
                    context.Items["Token"] = token;
                }
            }
            catch (Exception)
            {
                // If the token is invalid, throw an exception
                throw new AppException("The token is invalid");
            }
        }
        // Continue processing the request
        await _next(context);
    }
}
