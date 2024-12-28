namespace GuildWars2;

/// <summary>Empty lists, which are surprisingly useful sometimes.</summary>
internal static class Empty
{
    public static readonly IReadOnlyList<string> ListOfString = Array.Empty<string>();

    public static readonly IReadOnlyList<int> ListOfInt32 = Array.Empty<int>();

    public static IReadOnlyList<T> List<T>()
    {
        return Array.Empty<T>();
    }
}
