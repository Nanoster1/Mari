using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Mari.Contracts.Common.Routes.Server;
using Mari.Infrastructure.Authentication.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Mari.Server.Authentication.Configurations;

public static class OAuthConfig
{
    public const string AuthenticationScheme = "GitHub";
    public static void ConfigureOAuthOptions(IServiceCollection services, OAuthOptions options, IConfiguration configuration)
    {
        var oauthSettings = new OAuthSettings();
        configuration.GetSection(OAuthSettings.SectionName).Bind(oauthSettings);

        options.ClientId = oauthSettings.ClientId;
        options.ClientSecret = oauthSettings.ClientSecret;
        options.CorrelationCookie.SameSite = SameSiteMode.Lax;
        options.AuthorizationEndpoint = oauthSettings.AuthorizationEndpoint;
        options.TokenEndpoint = oauthSettings.TokenEndpoint;
        options.CallbackPath = new PathString(ServerRoutes.OAuthCallbackPath);
        options.UserInformationEndpoint = oauthSettings.UserInformationEndpoint;
        options.SaveTokens = true;
        foreach (var scope in oauthSettings.Scopes) options.Scope.Add(scope);
        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, oauthSettings.IdJsonKey);
        options.ClaimActions.MapJsonKey(ClaimTypes.Name, oauthSettings.NameJsonKey);
        options.Events = new OAuthEvents
        {
            OnCreatingTicket = async context =>
            {
                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                response.EnsureSuccessStatusCode();
                var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                context.RunClaimActions(json.RootElement);
            }
        };
    }
}
