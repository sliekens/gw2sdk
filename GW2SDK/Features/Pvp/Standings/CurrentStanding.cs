namespace GuildWars2.Pvp.Standings;

[PublicAPI]
[DataTransferObject]
public sealed record CurrentStanding
{
    public required int TotalPips { get; init; }

    public required int Division { get; init; }

    public required int Tier { get; init; }

    public required int Pips { get; init; }

    public required int Repeats { get; init; }

    public required int? Rating { get; init; }

    public required int? Decay { get; init; }
}
