using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mari.Server.Swagger;

public static class SwaggerConfig
{
    public static void SetUp(this SwaggerGenOptions options, IConfiguration config)
    {
        AddSwaggerDoc(options);
        //AddSecurityDefinition(options, config);
        //AddSecurityRequirement(options);
        options.CustomSchemaIds(t => t.ToString());
    }

    private static void AddSwaggerDoc(SwaggerGenOptions options)
    {
        var assemblyName = typeof(SwaggerConfig).Assembly.GetName();

        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Mari",
            Description = "Веб проект в рамках задания от компании \"Интерсвязь\"",
            Version = assemblyName.Version?.ToString() ?? "Без версии",
            Contact = new OpenApiContact
            {
                Name = "Небытов Даниил, Соколов Данил, Ефимов Иван",
                Url = new Uri("https://github.com/Nanoster1/MariTest.git")
            }
        });

        var xmlFile = $"{assemblyName.Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }

    // public static void AddSecurityDefinition(SwaggerGenOptions options, IConfiguration config)
    // {
    //     options.AddSecurityDefinition("oauth2",
    //         new OpenApiSecurityScheme
    //         {
    //             Type = SecuritySchemeType.OAuth2,
    //             Flows = new OpenApiOAuthFlows
    //             {
    //                 ClientCredentials = new OpenApiOAuthFlow
    //                 {
    //                     TokenUrl = new Uri(config.GetConnectionString("TokenUrl")),
    //                     Scopes = new Dictionary<string, string>()
    //                 }
    //             }
    //         });
    // }

    // public static void AddSecurityRequirement(SwaggerGenOptions options)
    // {
    //     options.AddSecurityRequirement(new OpenApiSecurityRequirement
    //     {
    //         {
    //             new OpenApiSecurityScheme
    //             {
    //                 Reference = new OpenApiReference
    //                 {
    //                     Type = ReferenceType.SecurityScheme,
    //                     Id = SecuritySchemeType.OAuth2.GetDisplayName()
    //                 }
    //             },
    //             new List<string>()
    //         }
    //     });
    // }

}
