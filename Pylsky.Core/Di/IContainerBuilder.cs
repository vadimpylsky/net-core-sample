using System;
using System.Reflection;

namespace Pylsky.Core.Di
{
    public interface IContainerBuilder
    {
        void PerDependency<TImplementation, TService>()
            where TImplementation : TService;

        void PerDependency<T>();

        void PerDependencyFactory(Type type, MemberInfo factoryMethod, Type factoryType);

        void Singleton<TImplementation, TService>()
            where TImplementation : TService;

        void Singleton<T>();
    }
}