namespace Pylsky.Infrastructure.Ioc;

public class InfrastructureConfiguration
{
    public InfrastructureConfiguration(string sqlitePath)
    {
        SqLitePath = sqlitePath;
    }

    public string SqLitePath { get; }
}