using System;
using System.Reflection;

namespace Pylsky.Core.Interfaces;

public interface IContainerBuilder
{
    public void PerDependency<T>();

    void PerDependency<TImplementation, TService>()
        where TImplementation : TService;

    void PerDependencyFactory(Type type, MemberInfo factoryMethod, Type factoryType);

    public void Singleton<T>();

    void Singleton<T>(Func<T> factory);

    void Singleton<TImplementation, TService>()
        where TImplementation : TService;

    void Singleton<TService, TImplementation>(Func<TImplementation> factory)
        where TImplementation : TService;
}