using Microsoft.EntityFrameworkCore;

namespace Pylsky.Infrastructure.Ef.Internal;

internal class DatabaseContextOptions
{
    public DatabaseContextOptions(string dataSource)
    {
        var builder = new DbContextOptionsBuilder();
        builder.UseSqlite($"Data Source={dataSource}");
        DbContextOptions = builder.Options;
    }

    public DbContextOptions DbContextOptions { get; }
}