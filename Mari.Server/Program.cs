using Mari.Application;
using Mari.Contracts.Common.Routes.Server;
using Mari.Infrastructure;
using Mari.Server.Authentication;
using Mari.Server.Mapping;
using Mari.Server.Settings;
using Mari.Server.Swagger;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;

var builder = WebApplication.CreateBuilder(args);
{
    StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddMariAuthentication(builder.Configuration);
    builder.Services.AddServerSettings(builder.Configuration);
    builder.Services.AddMapping();

    builder.Services.AddControllers();
    builder.Services.AddRazorPages();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options => options.SetUp(builder.Configuration));
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseWebAssemblyDebugging();
    }
    else
    {
        app.UseExceptionHandler(ServerRoutes.Controllers.Error);
    }

    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages();
    app.MapControllers();
    app.MapFallbackToFile(ServerRoutes.FallbackFile);
}

app.Run();
