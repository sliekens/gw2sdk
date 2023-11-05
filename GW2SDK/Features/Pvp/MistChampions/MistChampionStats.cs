namespace GuildWars2.Pvp.MistChampions;

[PublicAPI]
[DataTransferObject]
public sealed record MistChampionStats
{
    public required int Offense { get; init; }

    public required int Defense { get; init; }

    public required int Speed { get; init; }
}
