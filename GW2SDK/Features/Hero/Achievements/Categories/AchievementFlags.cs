namespace GuildWars2.Hero.Achievements.Categories;

/// <summary>Modifiers for achievements.</summary>
[PublicAPI]
public sealed record AchievementFlags : Flags
{
    /// <summary>The achievement is related to a festival celebration.</summary>
    public required bool SpecialEvent { get; init; }

    /// <summary>The achievement can only be progressed in PvE.</summary>
    public required bool PvE { get; init; }
}
