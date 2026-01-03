namespace GuildWars2.Guilds.Ranks;

/// <summary>Information about a guild rank.</summary>
[DataTransferObject]
public sealed record GuildRank
{
    /// <summary>The rank ID.</summary>
    public required string Id { get; init; }

    /// <summary>The display order of the rank in the guild ranks panel.</summary>
    public required int Order { get; init; }

    /// <summary>The permissions assigned to the rank.</summary>
    public required IReadOnlyList<string> Permissions { get; init; }

    /// <summary>The URL of the rank icon.</summary>
    public required Uri IconUrl { get; init; }
}
