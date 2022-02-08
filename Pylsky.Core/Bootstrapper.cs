using Pylsky.Core.Interfaces;
using Pylsky.Core.Services;

namespace Pylsky.Core;

public static class Bootstrapper
{
    public static void Configure(IContainerBuilder containerBuilder)
    {
        containerBuilder.PerDependency<ActivitiesService, IActivitiesService>();
    }
}