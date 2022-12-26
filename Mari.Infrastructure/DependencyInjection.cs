using Mari.Application.Common.Interfaces.Authentication;
using Mari.Application.Common.Interfaces.CommonServices;
using Mari.Application.Common.Interfaces.Persistence;
using Mari.Infrastructure.Authentication.Services;
using Mari.Infrastructure.Authentication.Settings;
using Mari.Infrastructure.CommonServices;
using Mari.Infrastructure.Persistence;
using Mari.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Mari.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddDbContext<MariDbContext>(options => options
            .UseNpgsql(configuration.GetConnectionString(MariDbContext.ConnectionString))
            .UseSnakeCaseNamingConvention());

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IReleaseRepository, ReleaseRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();

        return services;
    }
}
