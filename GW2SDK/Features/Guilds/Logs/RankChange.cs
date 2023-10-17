namespace GuildWars2.Guilds.Logs;

[PublicAPI]
[DataTransferObject]
public sealed record RankChange : GuildLog
{
    public required string User { get; init; }

    public required string OldRank { get; init; }

    public required string NewRank { get; init; }

    public required string ChangedBy { get; init; }
}