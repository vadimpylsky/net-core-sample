using Microsoft.EntityFrameworkCore.Design;

namespace Pylsky.Infrastructure.Ef.Internal.Design;

internal class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
#pragma warning disable CS8625
        return new DatabaseContext(new DatabaseContextOptions("/Users/vadimpylsky/.local/share/database.db"));
#pragma warning restore CS8625
    }
}