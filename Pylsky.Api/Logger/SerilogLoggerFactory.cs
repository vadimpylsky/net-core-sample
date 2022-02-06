using Pylsky.Core.Logger;

namespace Pylsky.Api.Logger;

public class SerilogLoggerFactory
{
    public IPylskyLogger<T> Create<T>()
    {
        return new SerilogLogger<T>();
    }
}