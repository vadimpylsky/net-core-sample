using MediatR;
using Pylsky.Core.Interfaces;

namespace Pylsky.Commands;

public static class Bootstrapper
{
    public static void Configure(IContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterMany(new[] {typeof(IMediator).Assembly, typeof(Bootstrapper).Assembly});
    }
}