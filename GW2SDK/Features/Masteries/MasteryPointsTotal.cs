namespace GuildWars2.Masteries;

[PublicAPI]
public sealed record MasteryPointsTotal
{
    public required string Region { get; init; }

    public required int Spent { get; init; }

    public required int Earned { get; init; }

    public int Available => Earned - Spent;
}
