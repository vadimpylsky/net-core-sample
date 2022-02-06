using System;
using Pylsky.Core.Logger;
using Serilog;
using Serilog.Events;

namespace Pylsky.Api.Logger;

public class SerilogLogger<T> : IPylskyLogger<T>
{
    private readonly ILogger _logger;

    public SerilogLogger()
    {
        _logger = Serilog.Log.ForContext<T>();
    }

    public void Log(string? message, Exception? exception)
    {
        _logger.Write(LogEventLevel.Information, exception, message);
    }
}