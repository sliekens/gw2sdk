namespace GuildWars2.Wvw.Guilds;

/// <summary>Information about a guild in World vs. World.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record WvwGuild
{
    /// <summary>The guild's name.</summary>
    public required string Name { get; init; }

    /// <summary>The guild's shard ID. Guilds with the same shard ID play for the same team.</summary>
    public required string ShardId { get; init; }
}
