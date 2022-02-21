using Pylsky.Core.Interfaces;
using Pylsky.Infrastructure.Ef.Internal;

namespace Pylsky.Infrastructure.Ef;

public static class Bootstrapper
{
    public static void Configure(IContainerBuilder containerBuilder, EfConfiguration configuration)
    {
        containerBuilder.PerDependency<SomeRepository, ISomeRepository>();
        containerBuilder.PerDependency<QueryableAggregate, IQueryableAggregate>();
        containerBuilder.PerDependency<DatabaseContext>();

        containerBuilder.Singleton(() => new DatabaseContextOptions(configuration.DbPath));
    }
}