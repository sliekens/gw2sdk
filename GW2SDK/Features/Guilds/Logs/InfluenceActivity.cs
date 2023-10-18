namespace GuildWars2.Guilds.Logs;

[PublicAPI]
[DataTransferObject]
public sealed record InfluenceActivity : GuildLog
{
    public required InfluenceActivityKind Activity { get; init; }

    public required int TotalParticipants { get; init; }

    public required IReadOnlyCollection<string> Participants { get; init; }
}
