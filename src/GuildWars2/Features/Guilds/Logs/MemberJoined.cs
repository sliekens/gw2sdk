namespace GuildWars2.Guilds.Logs;

/// <summary>A log entry about a member joining the guild.</summary>
[DataTransferObject]
public sealed record MemberJoined : GuildLogEntry
{
    /// <summary>The ID of the member who joined the guild.</summary>
    public required string User { get; init; }
}
