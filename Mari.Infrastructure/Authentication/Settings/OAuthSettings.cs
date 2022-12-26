namespace Mari.Infrastructure.Authentication.Settings;

public class OAuthSettings
{
    public const string SectionName = "OAuthSettings";
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string AuthorizationEndpoint { get; set; } = null!;
    public string TokenEndpoint { get; set; } = null!;
    public string CallbackPath { get; set; } = null!;
    public string UserInformationEndpoint { get; set; } = null!;
    public string IdJsonKey { get; set; } = null!;
    public string NameJsonKey { get; set; } = null!;
    public string[] Scopes { get; set; } = null!;
}
