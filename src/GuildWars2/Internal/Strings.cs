namespace GuildWars2;

internal static class Strings
{
    internal static string ToCsv(this IEnumerable<string> values)
    {
        return string.Join(",", values);
    }
}
