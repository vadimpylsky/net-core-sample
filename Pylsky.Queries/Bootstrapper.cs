using Pylsky.Core.Interfaces;

namespace Pylsky.Queries;

public static class Bootstrapper
{
    public static void Configure(IContainerBuilder containerBuilder)
    {
        containerBuilder.PerDependency<Internal.Queries, IQueries>();
    }
}