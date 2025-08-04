using System.Text.Json.Serialization;

using GuildWars2.Collections;

namespace GuildWars2.Hero.Achievements.Categories;

/// <summary>Modifiers for achievements.</summary>
[JsonConverter(typeof(AchievementFlagsJsonConverter))]
public sealed record AchievementFlags : Flags
{
    /// <summary>No modifiers.</summary>
    public static AchievementFlags None { get; } = new()
    {
        SpecialEvent = false,
        PvE = false,
        Other = new ValueList<string>(0)
    };

    /// <summary>The achievement is related to a festival celebration.</summary>
    public required bool SpecialEvent { get; init; }

    /// <summary>The achievement can only be progressed in PvE.</summary>
    public required bool PvE { get; init; }
}
