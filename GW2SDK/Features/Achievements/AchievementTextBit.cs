namespace GuildWars2.Achievements;

[PublicAPI]
public sealed record AchievementTextBit : AchievementBit
{
    public required string Text { get; init; }
}
