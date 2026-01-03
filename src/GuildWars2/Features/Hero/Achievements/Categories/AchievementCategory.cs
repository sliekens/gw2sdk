using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements.Categories;

/// <summary>Information about an achievement category.</summary>
[DataTransferObject]
[JsonConverter(typeof(AchievementCategoryJsonConverter))]
public sealed record AchievementCategory
{
    /// <summary>The achievement category ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the category as it appears in the sidebar of the achievement panel.</summary>
    public required string Name { get; init; }

    /// <summary>The description of the category as it appears in the tooltip of a category.</summary>
    /// <remarks>Can be empty.</remarks>
    public required string Description { get; init; }

    /// <summary>The display order of the category in the sidebar of the achievement panel.</summary>
    public required int Order { get; init; }

    /// <summary>The URL of the category icon as it appears in the achievement panel.</summary>
    [Obsolete("Use IconUrl instead.")]
    public required string IconHref { get; init; }

    /// <summary>The URL of the category icon as it appears in the achievement panel.</summary>
    public required Uri IconUrl { get; init; }

    /// <summary>The achievements in this category.</summary>
    /// <remarks>Can be empty.</remarks>
    public required IReadOnlyList<AchievementRef> Achievements { get; init; }

    /// <summary>Tomorrow's achievements in this category in the case of achievements that change on a daily basis.</summary>
    public required IReadOnlyList<AchievementRef>? Tomorrow { get; init; }
}
