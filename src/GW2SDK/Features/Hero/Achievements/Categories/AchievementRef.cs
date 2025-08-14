using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements.Categories;

/// <summary>A reference to an achievement.</summary>
[DataTransferObject]
[JsonConverter(typeof(AchievementRefJsonConverter))]
public sealed record AchievementRef
{
    /// <summary>The achievement ID.</summary>
    public required int Id { get; init; }

    /// <summary>Contains various modifiers that affect how achievements behave.</summary>
    public required AchievementFlags Flags { get; init; }

    /// <summary>The minimum and/or maximum level required to access this achievement.</summary>
    public required LevelRequirement? Level { get; init; }
}
