namespace GuildWars2;

internal static class Strings
{
    internal static string ToCsv(this IEnumerable<string> values) => string.Join(",", values);
}
