namespace GuildWars2.Guilds.Logs;

/// <summary>A log entry about a change in guild influence.</summary>
[DataTransferObject]
public sealed record InfluenceActivity : GuildLogEntry
{
    /// <summary>The nature of the influence activity.</summary>
    public required Extensible<InfluenceActivityKind> Activity { get; init; }

    /// <summary>How many members participated in the activity.</summary>
    public required int TotalParticipants { get; init; }

    /// <summary>The user IDs of the members who participated in the activity.</summary>
    public required IReadOnlyCollection<string> Participants { get; init; }
}
