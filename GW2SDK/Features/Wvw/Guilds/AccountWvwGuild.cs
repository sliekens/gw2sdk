namespace GuildWars2.Wvw.Guilds;

/// <summary>Information about a player account's World vs. World guild.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record AccountWvwGuild
{
    /// <summary>The player's World vs. World team ID.</summary>
    public required int? TeamId { get; init; }

    /// <summary>The account's selected World vs. World guild.</summary>
    public required string? GuildId { get; init; }
}
