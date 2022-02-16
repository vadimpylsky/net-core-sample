using Pylsky.Core.Interfaces;
using Pylsky.Infrastructure.Ef.Internal;

namespace Pylsky.Infrastructure.Ef;

public static class Bootstrapper
{
    public static void Configure(IContainerBuilder containerBuilder, EfConfiguration configuration)
    {
        containerBuilder.PerDependency<QueryableAggregate, IQueryableAggregate>();
        containerBuilder.PerDependency<SomeRepository, ISomeRepository>();

        containerBuilder.Singleton(() => new DatabaseContextOptions(configuration.DbPath));
        containerBuilder.Singleton<DatabaseContext>();
    }
}