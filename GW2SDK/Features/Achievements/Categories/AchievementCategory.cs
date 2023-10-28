namespace GuildWars2.Achievements.Categories;

/// <summary>Information about an achievement category.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record AchievementCategory
{
    /// <summary>The achievement category ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the category as it appears in the sidebar of the achievement panel.</summary>
    public required string Name { get; init; }

    /// <summary>The description of the category as it appears in the tooltip of a category.</summary>
    /// <remarks>Can be empty.</remarks>
    public required string Description { get; init; }

    /// <summary>The sort order of the category. This determines the order in which categories are displayed in the sidebar of
    /// the achievement panel.</summary>
    public required int Order { get; init; }

    /// <summary>The icon URI of the category.</summary>
    public required string Icon { get; init; }

    /// <summary>The achievements in this category.</summary>
    /// <remarks>Can be empty.</remarks>
    public required IReadOnlyList<AchievementRef> Achievements { get; init; }

    /// <summary>Tomorrow's achievements in this category in the case of achievements that change on a daily basis.</summary>
    public required IReadOnlyList<AchievementRef>? Tomorrow { get; init; }
}
