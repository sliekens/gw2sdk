namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record LeaderboardTierRange
{
    public required double Minimum { get; init; }

    public required double Maximum { get; init; }
}
