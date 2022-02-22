using System;
using Pylsky.Core.Interfaces;
using Serilog;
using Serilog.Events;

namespace Pylsky.Infrastructure.Logger;

internal class SerilogLogger<T> : IPylskyLogger<T>
{
    private readonly ILogger _logger;

    public SerilogLogger()
    {
        _logger = Log.ForContext<T>();
    }

    public void Info(string? message, Exception? exception)
    {
        _logger.Write(LogEventLevel.Information, exception, message);
    }

    public void Error(Exception ex)
    {
        _logger.Write(LogEventLevel.Error, ex, null);
    }
}