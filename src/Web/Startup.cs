using Application.DependencyInjection.Extensions;
using Persistence.DependencyInjection.Extensions;
using Presentation.DependencyInjection.Extensions;
using Serilog;
using Serilog.Events;
using Shared.DependencyInjection.Extensions;
using System.Net;
using System.Text.Json;

namespace Web;

public class Startup
{
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }
    public Startup(IWebHostEnvironment env)
    {
        this.Environment = env;

        this.Configuration = this.ConfigurationBuilder();

        this.CreateLogger();
    }

    // This method gets called by the runtime. Use this method to add serices to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSerilog();

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        #region architecture
        //services.AddPersistence(migrationAssembly: AssemblyReference.Assembly.GetName().Name);
        services.AddPersistence();
        services.AddApplication();
        services.AddPresentation();
        services.AddShared();
        #endregion

        //reqeust format camelCase
        services.AddControllersWithViews().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });

        //response format camelCase
        services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
        {
            options.SerializerOptions.PropertyNameCaseInsensitive = false;
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(WebApplication app)
    {
        app.UseSerilogRequestLogging();

        app.UseExceptionHandlingWrapper();

        app.UseLanguage();

        if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            app.UseConfigureSwagger();

        app.UseApplicationConfig();

        app.UseHttpsRedirection();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseJwtMiddleware();

        app.UseAuthorization();

#pragma warning disable ASP0014
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/helloworld", async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                await context.Response.WriteAsync("Hello World!");
            });

            endpoints.MapGet("/hello-world", async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                await context.Response.WriteAsync("Hello World!");
            });

            endpoints.MapControllers();
            endpoints.MapControllerRoute(
               name: "default",
               pattern: "{controller=Home}/{action=index}/{id?}");
        });

        app.Run(async (context) =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsync("404 - Resource Not Found");
        });
    }
    public IConfigurationRoot ConfigurationBuilder()
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Environment.ContentRootPath)
            // general properties
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            // specify the environment-based properties
            .AddJsonFile($"appsettings.{Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables("");
        //Configuration = configurationBuilder.Build();
        return configurationBuilder.Build();
    }
    private void CreateLogger()
    {
        RollingInterval rollingInterval = RollingInterval.Day;
        string outputTemplate = "[{Timestamp:u} {Level:u3}] {Message:lj}{NewLine}{Exception}";
        string common = "Logs";
        string extension = ".txt";
        Log.Logger = new Serilog.LoggerConfiguration().ReadFrom.Configuration(Configuration)
        .WriteTo.Logger(lc => lc.WriteTo.Console(outputTemplate: outputTemplate))
        .WriteTo.Logger(lc => lc
            .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Verbose)
            .WriteTo.File(path: @$"{common}\Verbose\{extension}", rollingInterval: rollingInterval, outputTemplate: outputTemplate))
        .WriteTo.Logger(lc => lc
            .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug)
            .WriteTo.File(path: @$"{common}\Debug\{extension}", rollingInterval: rollingInterval, outputTemplate: outputTemplate))
        .WriteTo.Logger(lc => lc
            .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
            .WriteTo.File(path: @$"{common}\Information\{extension}", rollingInterval: rollingInterval, outputTemplate: outputTemplate))
        .WriteTo.Logger(lc => lc
            .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
            .WriteTo.File(path: @$"{common}\Warning\{extension}", rollingInterval: rollingInterval, outputTemplate: outputTemplate))
        .WriteTo.Logger(lc => lc
            .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
            .WriteTo.File(path: @$"{common}\Error\{extension}", rollingInterval: rollingInterval, outputTemplate: outputTemplate))
        .WriteTo.Logger(lc => lc
            .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal)
            .WriteTo.File(path: @$"{common}\Fatal\{extension}", rollingInterval: rollingInterval, outputTemplate: outputTemplate))
        .CreateLogger();
    }
}
