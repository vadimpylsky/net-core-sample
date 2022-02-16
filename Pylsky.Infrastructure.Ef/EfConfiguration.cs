namespace Pylsky.Infrastructure.Ef;

public class EfConfiguration
{
    public EfConfiguration(string dbPath)
    {
        DbPath = dbPath;
    }

    public string DbPath { get; }
}