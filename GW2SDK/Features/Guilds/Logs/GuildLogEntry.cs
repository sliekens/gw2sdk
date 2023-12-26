namespace GuildWars2.Guilds.Logs;

/// <summary>An entry in the guild's log, used for audit purposes. This class is the base type for all log entries. Cast
/// objects of this type to a more specific type to access more properties.</summary>
[PublicAPI]
[DataTransferObject]
[Inheritable]
public record GuildLogEntry
{
    /// <summary>The log ID. It can be used as a skip token for paginated results.</summary>
    public required int Id { get; init; }

    /// <summary>The time when the log entry was created.</summary>
    public required DateTimeOffset Time { get; init; }
}
