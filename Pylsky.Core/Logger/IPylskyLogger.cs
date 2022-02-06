using System;

namespace Pylsky.Core.Logger
{
    public interface IPylskyLogger<out T> : IPylskyLogger
    {
    }

    public interface IPylskyLogger
    {
        void Log(string? message = null, Exception? exception = null);
    }
}