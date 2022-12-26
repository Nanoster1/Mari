using Mari.Client.Common.Http.DelegatingHandlers;
using Mari.Client.Common.Http.ProblemsHandling;
using Mari.Http.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Mari.Client.Common.Http;

public static class DependencyInjection
{
    public static IServiceCollection AddHttpUtils(this IServiceCollection services, IWebAssemblyHostEnvironment environment)
    {
        services.AddScoped<HttpSender>(sp => new HttpSender(sp.GetService<IHttpClientFactory>()!)
        {
            HttpClientName = HttpClientNames.Api
        });

        services.AddProblemHandler(new()
        {
            DefaultProblemEvent = _ => Task.CompletedTask,
            ValidationProblemEvent = _ => Task.CompletedTask,
            UnauthorizedProblemEvent = _ => Task.CompletedTask,
            NotFoundProblemEvent = _ => Task.CompletedTask,
            ErrorProblemEvent = _ => Task.CompletedTask
        });

        services.AddTransient<ApiAuthorizationHeaderHandler>();

        services.AddHttpClient(HttpClientNames.Api, client =>
        {
            client.BaseAddress = new Uri(environment.BaseAddress);
        })
        .AddHttpMessageHandler<ApiAuthorizationHeaderHandler>();

        services.AddScoped(sp =>
            sp.GetService<IHttpClientFactory>()!
            .CreateClient(HttpClientNames.Api));
        return services;
    }

    public static IServiceCollection AddProblemHandler(this IServiceCollection services, ProblemHandlerOptions options)
    {
        services.AddSingleton<ProblemHandler>(sp => new ProblemHandler(options));
        return services;
    }
}
