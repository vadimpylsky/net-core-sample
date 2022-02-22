using System;
using System.Collections.Generic;
using System.Reflection;

namespace Pylsky.Core.Interfaces;

public interface IContainerBuilder
{
    public void PerDependency<T>();

    void PerDependency<TImplementation, TService>()
        where TImplementation : TService;

    void PerDependencyFactory(Type type, MemberInfo factoryMethod, Type factoryType);

    void Singleton<T>(Func<T> factory);

    void RegisterMany(IEnumerable<Assembly> assemblies);
}