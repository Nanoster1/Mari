namespace Mari.Server.Settings;

public static class DependencyInjection
{
    public static IServiceCollection AddServerSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<HostSettings>()
            .Bind(configuration.GetSection(HostSettings.SectionName));

        return services;
    }
}
