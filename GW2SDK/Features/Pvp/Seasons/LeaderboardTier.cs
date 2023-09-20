namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record LeaderboardTier
{
    public required string Color { get; init; }

    public required LeaderboardTierKind Kind { get; init; }

    public required string Name { get; init; }

    public required LeaderboardTierRange Range { get; init; }
}
