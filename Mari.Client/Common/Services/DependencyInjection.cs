using Mari.Client.Common.Interfaces;
using Mari.Client.Common.Interfaces.Formatters;
using Mari.Client.Common.Interfaces.Managers;
using Mari.Client.Common.Services.Formatters;
using Mari.Client.Common.Services.Managers;
using Microsoft.AspNetCore.Components.Authorization;

namespace Mari.Client.Common.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddClientServices(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationStateProvider, MariAuthStateProvider>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IReleaseManager, ReleaseManager>();
        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<ICurrentUserManager, CurrentUserManager>();
        services.AddScoped<ICommentManager, CommentManager>();
        services.AddScoped<IDescriptionFormatter, RegexDescriptionFormatter>();
        services.AddScoped<IDateFormatter, DateFormatter>();
        return services;
    }
}
