namespace GuildWars2.Pvp.Heroes;

[PublicAPI]
[DataTransferObject]
public sealed record HeroStats
{
    public required int Offense { get; init; }

    public required int Defense { get; init; }

    public required int Speed { get; init; }
}
