WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
Web.Startup startup = new Web.Startup(builder.Environment);
startup.ConfigureServices(builder.Services);
WebApplication app = builder.Build();
startup.Configure(app);
app.Run();