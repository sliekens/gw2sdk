namespace GuildWars2.Achievements.Groups;

/// <summary>Information about an achievement group, which is a list of achievement categories.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record AchievementGroup
{
    /// <summary>The achievement group ID.</summary>
    public required string Id { get; init; }

    /// <summary>The name of the group as it appears in the sidebar of the achievement panel.</summary>
    public required string Name { get; init; }

    /// <summary>The description of the group as it appears in the tooltip of a group.</summary>
    /// <remarks>Can be empty.</remarks>
    public required string Description { get; init; }

    /// <summary>The sort order of the group. This determines the order in which groups are displayed in the sidebar of the
    /// achievement panel.</summary>
    public required int Order { get; init; }

    /// <summary>The achievement categories in this group.</summary>
    public required IReadOnlyList<int> Categories { get; init; }
}
