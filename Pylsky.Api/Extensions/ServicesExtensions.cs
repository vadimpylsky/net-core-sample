using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Pylsky.Api.Extensions;

internal static class ServicesExtensions
{
    private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    public static void AddFirebaseAuth(this IServiceCollection services)
    {
        //TODO: move to config
        const string appId = "bugs-dev-befd3";

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = $"https://securetoken.google.com/{appId}";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"https://securetoken.google.com/{appId}",
                    ValidateAudience = true,
                    ValidAudience = appId,
                    ValidateLifetime = true
                };
            });
    }

    public static void AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(MyAllowSpecificOrigins,
                policyBuilder =>
                {
                    policyBuilder.WithOrigins("http://localhost")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(_ => true);
                });
        });
    }

    public static void AddCustomSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT. Sample: 'Bearer [jwt]'",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            s.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    public static void UseSerilog(this ConfigureHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
        );
    }

    public static void UseCustomCors(this WebApplication app)
    {
        app.UseCors(MyAllowSpecificOrigins);
    }
}