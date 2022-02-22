using System;
using System.Collections.Generic;
using System.Reflection;
using DryIoc;
using MediatR;
using Pylsky.Core.Interfaces;

namespace Pylsky.Infrastructure.Ioc.Internal;

internal class DryIocContainerBuilder : IContainerBuilder
{
    private readonly Container _container;

    public DryIocContainerBuilder(Container container)
    {
        _container = container;
        _container.RegisterDelegate<ServiceFactory>(r => r.Resolve);
    }

    public void PerDependency<T>()
    {
        _container.Register<T>(Reuse.Transient);
    }

    public void PerDependency<TImplementation, TService>() where TImplementation : TService
    {
        _container.Register<TService, TImplementation>(Reuse.Transient);
    }

    public void Singleton<T>(Func<T> factory)
    {
        _container.RegisterDelegate(factory, Reuse.Singleton);
    }

    public void PerDependencyFactory(Type type, MemberInfo factoryMethod, Type factoryType)
    {
        _container.Register(type,
            made: Made.Of(
                factoryMethod,
                ServiceInfo.Of(factoryType)));
    }

    public void RegisterMany(IEnumerable<Assembly> assemblies)
    {
        _container.RegisterMany(assemblies, Registrator.Interfaces);
    }
}