using System;
using System.Collections.Generic;

namespace Pylsky.Api.Extensions;

internal static class HttpContextItemsExtensions
{
    private const string UserKey = "internal_user_id";

    public static void AddUserGuid(this IDictionary<object, object?> items, Guid guid)
    {
        items[UserKey] = guid.ToString();
    }

    public static Guid GetUserGuid(this IDictionary<object, object?> items)
    {
        var item = items[UserKey] as string;
        return Guid.Parse(item!);
    }
}