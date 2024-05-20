using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Abstractions;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public abstract class ApiController : ControllerBase
{
    private ISender _sender;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();
}
