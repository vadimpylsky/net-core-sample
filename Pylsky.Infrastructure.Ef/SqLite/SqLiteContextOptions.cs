using Microsoft.EntityFrameworkCore;

namespace Pylsky.Infrastructure.Ef.SqLite;

internal class SqLiteContextOptions
{
    public SqLiteContextOptions(string dataSource)
    {
        var builder = new DbContextOptionsBuilder();
        builder.UseSqlite($"Data Source={dataSource}");
        DbContextOptions = builder.Options;
    }

    public DbContextOptions DbContextOptions { get; }
}