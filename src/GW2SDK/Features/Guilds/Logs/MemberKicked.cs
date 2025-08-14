namespace GuildWars2.Guilds.Logs;

/// <summary>A log entry about a member being kicked from the guild.</summary>
[DataTransferObject]
public sealed record MemberKicked : GuildLogEntry
{
    /// <summary>The ID of the user who was kicked.</summary>
    public required string User { get; init; }

    /// <summary>The ID of the user who kicked the member.</summary>
    public required string KickedBy { get; init; }
}
