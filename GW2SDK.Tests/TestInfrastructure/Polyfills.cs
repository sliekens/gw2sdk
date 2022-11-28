namespace GuildWars2.Tests.TestInfrastructure;

internal static class Polyfills
{
#if NET48
    public static void Deconstruct<TKey, TValue>(
        this System.Collections.Generic.KeyValuePair<TKey, TValue> kvp,
        out TKey key,
        out TValue value
    )
    {
        key = kvp.Key;
        value = kvp.Value;
    }
#endif
}
