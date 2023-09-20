namespace GuildWars2.Achievements;

[PublicAPI]
[DataTransferObject]
public sealed record AchievementTier
{
    public required int Count { get; init; }

    public required int Points { get; init; }
}
