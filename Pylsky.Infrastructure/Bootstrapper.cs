using Pylsky.Core.Extensions;
using Pylsky.Core.Interfaces;
using Pylsky.Infrastructure.Logger;

namespace Pylsky.Infrastructure;

public static class Bootstrapper
{
    public static void Configure(IContainerBuilder containerBuilder)
    {
        containerBuilder.PerDependency<SerilogLoggerFactory>();

        containerBuilder.PerDependencyFactory(
            typeof(IPylskyLogger<>),
            typeof(SerilogLoggerFactory).SingleMethod(nameof(SerilogLoggerFactory.Create)),
            typeof(SerilogLoggerFactory));
    }
}