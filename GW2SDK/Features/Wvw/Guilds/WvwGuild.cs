namespace GuildWars2.Wvw.Guilds;

/// <summary>Information about a guild in World vs. World.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record WvwGuild
{
    /// <summary>The guild's name.</summary>
    public required string Name { get; init; }

    /// <summary>The guild's WvW team ID.</summary>
    public required string TeamId { get; init; }
}
