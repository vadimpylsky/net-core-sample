using Microsoft.EntityFrameworkCore.Design;

namespace Pylsky.Infrastructure.Ef.SqLite.Design;

internal class ActivitiesContextFactory : IDesignTimeDbContextFactory<ActivitiesContext>
{
    public ActivitiesContext CreateDbContext(string[] args)
    {
#pragma warning disable CS8625
        return new ActivitiesContext(null, new SqLiteContextOptions("/Users/vadimpylsky/.local/share/test.db"));
#pragma warning restore CS8625
    }
}