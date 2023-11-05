namespace GuildWars2.Hero.Achievements;

/// <summary>Information about an achievement tier.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record AchievementTier
{
    /// <summary>The amount of progress needed to complete this tier. Context is required to understand this number. For
    /// example if the achievement is to kill 40 enemies, this would be 40.</summary>
    public required int Count { get; init; }

    /// <summary>The amount of achievement points awarded for completing this tier.</summary>
    public required int Points { get; init; }
}
