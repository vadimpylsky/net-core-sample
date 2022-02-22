using DryIoc;
using Pylsky.Core.Interfaces;
using Pylsky.Infrastructure.Ef;
using Pylsky.Infrastructure.Ioc.Internal;

namespace Pylsky.Infrastructure.Ioc;

public static class Bootstrapper
{
    public static void Configure(Container container, InfrastructureConfiguration configuration)
    {
        var containerBuilder = new DryIocContainerBuilder(container);

        Core.Bootstrapper.Configure(containerBuilder);
        Infrastructure.Bootstrapper.Configure(containerBuilder);
        var efConfig = new EfConfiguration(configuration.SqLitePath);
        Ef.Bootstrapper.Configure(containerBuilder, efConfig);
        Queries.Bootstrapper.Configure(containerBuilder);
        Commands.Bootstrapper.Configure(containerBuilder);

        containerBuilder.Singleton<IServiceResolver>(() => new ServiceResolver(container));
    }
}