using Microsoft.AspNetCore.Authentication.Cookies;

namespace Mari.Server.Authentication.Configurations;

public static class CookieConfig
{
    public const string AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    public static void ConfigureCookieOptions(CookieAuthenticationOptions options)
    {
    }
}
