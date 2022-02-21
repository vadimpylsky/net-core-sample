namespace Pylsky.Core.Interfaces;

public interface IServiceResolver
{
    T Resolve<T>();
}