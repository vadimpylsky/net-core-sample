using System;
using System.Reflection;
using DryIoc;
using Pylsky.Core.Di;

namespace Pylsky.Api.Di;

public class DryIocContainerBuilder : IContainerBuilder
{
    private readonly Container _container;

    public DryIocContainerBuilder(Container container)
    {
        _container = container;
    }

    public void PerDependency<TImplementation, TService>() where TImplementation : TService
    {
        RegisterInternal<TImplementation, TService>(Reuse.Transient);
    }

    public void PerDependency<T>()
    {
        RegisterInternal<T>(Reuse.Transient);
    }

    public void Singleton<TImplementation, TService>() where TImplementation : TService
    {
        RegisterInternal<TImplementation, TService>(Reuse.Singleton);
    }

    public void Singleton<T>()
    {
        _container.Register<T>(Reuse.Singleton);
    }

    public void PerDependencyFactory(Type type, MemberInfo factoryMethod, Type factoryType)
    {
        _container.Register(type,
            made: Made.Of(
                factoryMethod,
                ServiceInfo.Of(factoryType)));
    }

    private void RegisterInternal<TImplementation, TService>(IReuse reuse)
        where TImplementation : TService
    {
        _container.Register<TService, TImplementation>(reuse);
    }

    private void RegisterInternal<T>(IReuse reuse)
    {
        _container.Register<T>(reuse);
    }
}