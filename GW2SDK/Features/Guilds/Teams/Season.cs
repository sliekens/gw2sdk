namespace GuildWars2.Guilds.Teams;

[PublicAPI]
[DataTransferObject]
public sealed record Season
{
    public required string Id { get; init; }

    public required int Wins { get; init; }

    public required int Losses { get; init; }

    public required int Rating { get; init; }
}
