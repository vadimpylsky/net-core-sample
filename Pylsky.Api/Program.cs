using System;
using System.IO;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pylsky.Infrastructure.Ioc;
using Serilog;
using Serilog.Events;

namespace Pylsky.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        try
        {
            RunApp(args);
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void RunApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(s =>
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

        builder.Services.AddFirebaseAuth();

        const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(myAllowSpecificOrigins,
                policyBuilder =>
                {
                    policyBuilder.WithOrigins("http://localhost")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(_ => true);
                });
        });

        builder.Host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
        );

        builder.Host.UseServiceProviderFactory(new DryIocServiceProviderFactory());
        builder.Host.ConfigureContainer<Container>(ConfigureContainer);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(myAllowSpecificOrigins);
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

    private static void ConfigureContainer(HostBuilderContext context, Container container)
    {
        var sqlitePath = Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            context.Configuration["DatabaseName"]);

        var configuration = new InfrastructureConfiguration(sqlitePath);

        Bootstrapper.Configure(container, configuration);
    }
}

internal static class ServicesExtensions
{
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
}