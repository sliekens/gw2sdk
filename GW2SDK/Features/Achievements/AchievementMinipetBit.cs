namespace GuildWars2.Achievements;

[PublicAPI]
public sealed record AchievementMinipetBit : AchievementBit
{
    public required int Id { get; init; }
}
