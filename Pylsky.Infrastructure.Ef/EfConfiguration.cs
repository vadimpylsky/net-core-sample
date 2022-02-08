namespace Pylsky.Infrastructure.Ef;

public class EfConfiguration
{
    public EfConfiguration(string sqlitePath)
    {
        SqlitePath = sqlitePath;
    }

    public string SqlitePath { get; }
}