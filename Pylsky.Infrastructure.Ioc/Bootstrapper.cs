using DryIoc;
using Pylsky.Infrastructure.Ef;

namespace Pylsky.Infrastructure.Ioc;

public static class Bootstrapper
{
    public static void Configure(Container container, PylskyConfiguration configuration)
    {
        var containerBuilder = new DryIocContainerBuilder(container);

        Infrastructure.Bootstrapper.Configure(containerBuilder);

        var efConfiguration = new EfConfiguration(configuration.SqLitePath);

        Ef.Bootstrapper.Configure(containerBuilder, efConfiguration);
    }
}