using Mari.Server.Authentication.Configurations;

namespace Mari.Server.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection AddMariAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options => AuthenticationConfig.ConfigureAuthenticationOptions(options))
            .AddCookie(CookieConfig.AuthenticationScheme, options => CookieConfig.ConfigureCookieOptions(options))
            .AddOAuth(OAuthConfig.AuthenticationScheme, options => OAuthConfig.ConfigureOAuthOptions(services, options, configuration))
            .AddJwtBearer(JwtConfig.AuthenticationScheme, options => JwtConfig.ConfigureJwtBearerOptions(services, options, configuration));

        return services;
    }
}
