namespace GuildWars2.Hero.Achievements;

/// <summary>The base type for achievement rewards. Cast objects of this type to a more specific type to access more
/// properties.</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record AchievementReward;
