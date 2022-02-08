namespace Pylsky.Infrastructure.Ioc;

public class PylskyConfiguration
{
    public PylskyConfiguration(string sqlitePath)
    {
        SqLitePath = sqlitePath;
    }

    public string SqLitePath { get; }
}