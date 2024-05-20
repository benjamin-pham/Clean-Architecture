using Application.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Libraries;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class AppAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public AppAuthorizeAttribute() { }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        ForbiddenException exception = new ForbiddenException("Forbidden");

        User user = UserContext.Instance;
        if (user == null)
        {
            throw exception;
        }

        //check permission
    }
}