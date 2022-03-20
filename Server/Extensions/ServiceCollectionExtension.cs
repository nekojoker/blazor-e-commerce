using System;
using Microsoft.OpenApi.Models;

namespace BlazorEC.Server.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(o =>
        {
            string instance = configuration["AzureAdB2C:Instance"];
            string domain = configuration["AzureAdB2C:Domain"];
            string signInPolicyId = configuration["AzureAdB2C:SignUpSignInPolicyId"];
            string swaggerScope = configuration["Swagger:Scopes"];
            string swaggerClientId = configuration["Swagger:ClientId"];

            o.SwaggerDoc("v1", new OpenApiInfo { Title = "BlazorEC", Version = "v1" });

            o.AddSecurityDefinition("Azure AD B2C - Authorization", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{instance}{domain}/{signInPolicyId}/oauth2/v2.0/authorize"),
                        TokenUrl = new Uri($"{instance}{domain}/{signInPolicyId}/oauth2/v2.0/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            ["openid"] = "Sign In Permission",
                            [$"https://{domain}/{swaggerClientId}/{swaggerScope}"] = "Api Permission"
                        },
                    },
                },
                Description = "Azure AD B2C - Authorization",
                In = ParameterLocation.Header,
                Name = "Authorization",
            });

            o.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id ="Azure AD B2C - Authorization"
                        },
                    },
                    new string [] {  }
                },
            });
        });

        return services;
    }
}

