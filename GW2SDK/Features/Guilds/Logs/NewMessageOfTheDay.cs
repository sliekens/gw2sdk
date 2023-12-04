namespace GuildWars2.Guilds.Logs;

/// <summary>A log entry about a change in the guild message of the day.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record NewMessageOfTheDay : GuildLogEntry
{
    /// <summary>The ID of the user who changed the message of the day.</summary>
    public required string User { get; init; }

    /// <summary>The new message of the day.</summary>
    public required string MessageOfTheDay { get; init; }
}
