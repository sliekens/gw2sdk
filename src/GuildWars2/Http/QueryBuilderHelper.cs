#if !NET
namespace GuildWars2.Http;

internal static class QueryBuilderHelper
{
    internal static void Deconstruct(this KeyValuePair<string, string> instance, out string key, out string value)
    {
        key = instance.Key;
        value = instance.Value;
    }
}
#endif
