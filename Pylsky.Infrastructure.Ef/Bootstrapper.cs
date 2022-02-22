using Pylsky.Core.Interfaces;
using Pylsky.Infrastructure.Ef.Internal;

namespace Pylsky.Infrastructure.Ef;

public static class Bootstrapper
{
    public static void Configure(IContainerBuilder containerBuilder, EfConfiguration configuration)
    {
        containerBuilder.PerDependency<FixesRepository, IFixesRepository>();
        containerBuilder.PerDependency<UsersRepository, IUsersRepository>();
        containerBuilder.PerDependency<QueryableAggregate, IQueryableAggregate>();
        containerBuilder.PerDependency<DatabaseContext>();

        containerBuilder.Singleton(() => new DatabaseContextOptions(configuration.DbPath));
    }
}