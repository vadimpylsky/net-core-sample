using System;
using System.Linq;
using System.Reflection;

namespace Pylsky.Core.Extensions;

public static class ReflectionExtensions
{
    public static MethodInfo SingleMethod(this Type type, string name)
    {
        var result = type
            .GetTypeInfo()
            .DeclaredMethods
            .FirstOrDefault(method => method.IsPublic && method.Name == name);

        if (result == null)
        {
            throw new NullReferenceException("can't find method by provided name");
        }

        return result;
    }
}