using Mari.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Mari.Migrations;

public class Factory : IDesignTimeDbContextFactory<MariDbContext>
{
    public MariDbContext CreateDbContext(string[] args)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();

        var configuration = configurationBuilder.Build();
        var connectionString = configuration.GetConnectionString(MariDbContext.ConnectionString);
        var optionsBuilder = new DbContextOptionsBuilder<MariDbContext>()
            .UseNpgsql(connectionString,
                builder => builder.MigrationsAssembly(typeof(Factory).Assembly.GetName().Name))
            .UseSnakeCaseNamingConvention();

        return new MariDbContext(optionsBuilder.Options);
    }
}
