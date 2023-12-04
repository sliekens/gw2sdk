namespace GuildWars2.Guilds.Logs;

/// <summary>A log entry about a change in guild rank.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record RankChange : GuildLogEntry
{
    /// <summary>The ID of the user who was promoted or demoted.</summary>
    public required string User { get; init; }

    /// <summary>The old rank of the user.</summary>
    public required string OldRank { get; init; }

    /// <summary>The new rank of the user.</summary>
    public required string NewRank { get; init; }

    /// <summary>The ID of the user who performed the change.</summary>
    public required string ChangedBy { get; init; }
}
