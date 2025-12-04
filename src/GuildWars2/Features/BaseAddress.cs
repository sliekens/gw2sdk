namespace GuildWars2;

/// <summary>The base address of the Guild Wars 2 API.</summary>
public static class BaseAddress
{
    /// <summary>The default base address to use for API requests.</summary>
    public const string Default = "https://api.guildwars2.com";

    /// <summary>The default base address to use for API requests.</summary>
    public static readonly Uri DefaultUri = new(Default, UriKind.Absolute);
}
