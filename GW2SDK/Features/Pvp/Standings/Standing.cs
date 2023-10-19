namespace GuildWars2.Pvp.Standings;

[PublicAPI]
[DataTransferObject]
public sealed record Standing
{
    public required string SeasonId { get; init; }

    public required CurrentStanding Current { get; init; }

    public required BestStanding Best { get; init; }
}
