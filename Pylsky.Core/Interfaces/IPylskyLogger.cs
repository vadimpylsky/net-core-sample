using System;

namespace Pylsky.Core.Interfaces;

public interface IPylskyLogger<out T> : IPylskyLogger
{
}

public interface IPylskyLogger
{
    void Info(string? message = null, Exception? exception = null);
    void Error(Exception ex);
}