namespace GuildWars2.Guilds.Logs;

/// <summary>A log entry about a guild invite being declined.</summary>
[DataTransferObject]
public sealed record InviteDeclined : GuildLogEntry
{
    /// <summary>The ID of the user who sent the invite.</summary>
    public required string User { get; init; }

    /// <summary>The ID of the user who declined the invite.</summary>
    /// <remarks>Can be empty.</remarks>
    public required string DeclinedBy { get; init; }
}
