#if NET48
using System.Collections.Generic;

namespace GuildWars2.Tests.TestInfrastructure;

internal static class Polyfills
{
    public static void Deconstruct<TKey, TValue>(
        this KeyValuePair<TKey, TValue> kvp,
        out TKey key,
        out TValue value
    )
    {
        key = kvp.Key;
        value = kvp.Value;
    }
}
#endif
