namespace GuildWars2.Guilds.Teams;

[PublicAPI]
[DataTransferObject]
public sealed record Results
{
    public required int Wins { get; init; }
    public required int Losses { get; init; }
    public required int Desertions { get; init; }
    public required int Byes { get; init; }
    public required int Forfeits { get; init; }
}
