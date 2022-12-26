using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Mari.Server.Authentication.Configurations;

public static class AuthenticationConfig
{
    public static void ConfigureAuthenticationOptions(AuthenticationOptions options)
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }
}


