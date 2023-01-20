using System;
using Mari.Desktop.Paths;
using Mari.Desktop.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mari.Desktop;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddConfiguration();
        services.AddViewModels();
        return services;
    }

    public static IServiceProvider UseApplication(this IServiceProvider provider)
    {
        return provider;
    }

    private static IServiceCollection AddConfiguration(this IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(ConfigurationPaths.AppSettingsPath, optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        services.AddSingleton<IConfiguration>(configuration);
        return services;
    }
}
