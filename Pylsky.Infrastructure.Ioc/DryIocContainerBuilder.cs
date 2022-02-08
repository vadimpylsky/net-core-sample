using System;
using System.Reflection;
using DryIoc;
using Pylsky.Core.Interfaces;

namespace Pylsky.Infrastructure.Ioc;

internal class DryIocContainerBuilder : IContainerBuilder
{
    private readonly Container _container;

    public DryIocContainerBuilder(Container container)
    {
        _container = container;
    }

    public void PerDependency<T>()
    {
        _container.Register<T>(Reuse.Transient);
    }

    public void PerDependency<TImplementation, TService>() where TImplementation : TService
    {
        RegisterInternal<TImplementation, TService>(Reuse.Transient);
    }

    public void Singleton<T>()
    {
        _container.Register<T>(Reuse.Singleton);
    }

    public void Singleton<T>(Func<T> factory)
    {
        _container.RegisterDelegate(factory, Reuse.Singleton);
    }

    public void Singleton<TImplementation, TService>() where TImplementation : TService
    {
        RegisterInternal<TImplementation, TService>(Reuse.Singleton);
    }

    public void PerDependencyFactory(Type type, MemberInfo factoryMethod, Type factoryType)
    {
        _container.Register(type,
            made: Made.Of(
                factoryMethod,
                ServiceInfo.Of(factoryType)));
    }

    public void Singleton<TService, TImplementation>(Func<TImplementation> factory)
        where TImplementation : TService
    {
        _container.RegisterDelegate<TService>(_ => factory.Invoke(), Reuse.Singleton);
    }

    private void RegisterInternal<TImplementation, TService>(IReuse reuse)
        where TImplementation : TService
    {
        _container.Register<TService, TImplementation>(reuse);
    }
}