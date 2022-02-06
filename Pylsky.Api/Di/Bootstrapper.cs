using DryIoc;
using Pylsky.Api.Logger;
using Pylsky.Core.Di;
using Pylsky.Core.Logger;

namespace Pylsky.Api.Di;

public static class Bootstrapper
{
    public static void Configure(IContainerBuilder containerBuilder)
    {
        Core.Di.Bootstrapper.Configure(containerBuilder);

        containerBuilder.PerDependency<SerilogLoggerFactory>();

        containerBuilder.PerDependencyFactory(
            typeof(IPylskyLogger<>),
            typeof(SerilogLoggerFactory).GetSingleMethodOrNull(nameof(SerilogLoggerFactory.Create)),
            typeof(SerilogLoggerFactory));
        
        //TODO: configure api dependencies here
    }
}