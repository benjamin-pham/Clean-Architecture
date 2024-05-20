using Application.DependencyInjection.Options;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Presentation.Abstractions;

namespace Presentation.Controllers.V1;
[ApiVersion(1)]
public class HelloWorldController:ApiController
{
    AppSettings appSettings;
    public HelloWorldController(IOptions<AppSettings> appsettings)
    {
        appSettings = appsettings.Value;
    }
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello World!");
    }
    [HttpGet("environment")]
    public IActionResult GetEnvironment()
    {
        var envName = appSettings.EnvironmentName;
        return Ok(envName);
    }
}
