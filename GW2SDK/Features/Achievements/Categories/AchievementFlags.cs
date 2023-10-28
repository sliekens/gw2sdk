namespace GuildWars2.Achievements.Categories;

[PublicAPI]
public sealed record AchievementFlags
{
    /// <summary>The achievement is only active during a festival.</summary>
    public required bool SpecialEvent { get; init; }

    /// <summary>The achievement can only be progressed in PvE.</summary>
    public required bool PvE { get; init; }

    /// <summary>Other undocumented flags. If you find out what they mean, please open an issue or a pull request.</summary>
    public required IReadOnlyCollection<string> Other { get; init; }
}
