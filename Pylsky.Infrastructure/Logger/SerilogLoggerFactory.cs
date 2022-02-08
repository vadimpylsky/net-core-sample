using Pylsky.Core.Interfaces;

namespace Pylsky.Infrastructure.Logger;

internal class SerilogLoggerFactory
{
    public IPylskyLogger<T> Create<T>()
    {
        return new SerilogLogger<T>();
    }
}