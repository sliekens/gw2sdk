using JetBrains.Annotations;

namespace GuildWars2.Achievements;

[PublicAPI]
public sealed record AchievementItemBit : AchievementBit
{
    public required int Id { get; init; }
}
