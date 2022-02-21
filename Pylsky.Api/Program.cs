using System;
using System.IO;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pylsky.Api.Extensions;
using Pylsky.Api.Middlewares;
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
        builder.Services.AddCustomSwaggerGen();
        builder.Services.AddFirebaseAuth();
        builder.Services.AddCustomCors();

        builder.Host.UseSerilog();
        builder.Host.UseServiceProviderFactory(new DryIocServiceProviderFactory());
        builder.Host.ConfigureContainer<Container>(ConfigureContainer);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCustomCors();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<MapToInternalUserMiddleware>();
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