using Pylsky.Core.Interfaces;
using Pylsky.Infrastructure.Ef.SqLite;

namespace Pylsky.Infrastructure.Ef;

public static class Bootstrapper
{
    public static void Configure(IContainerBuilder containerBuilder, EfConfiguration configuration)
    {
        containerBuilder.Singleton(() => new SqLiteContextOptions(configuration.SqlitePath));
        containerBuilder.Singleton<ActivitiesContext, IActivitiesDataSource>();
    }
}