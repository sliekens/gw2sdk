using JetBrains.Annotations;

namespace GuildWars2.Achievements;

[PublicAPI]
public sealed record AchievementSkinBit : AchievementBit
{
    public required int Id { get; init; }
}
