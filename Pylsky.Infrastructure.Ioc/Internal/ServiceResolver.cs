using DryIoc;
using Pylsky.Core.Interfaces;

namespace Pylsky.Infrastructure.Ioc.Internal;

internal class ServiceResolver : IServiceResolver
{
    private readonly Container _container;

    public ServiceResolver(Container container)
    {
        _container = container;
    }

    public T Resolve<T>()
    {
        return _container.Resolve<T>();
    }
}