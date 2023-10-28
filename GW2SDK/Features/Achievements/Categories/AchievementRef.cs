namespace GuildWars2.Achievements.Categories;

/// <summary>A reference to an achievement.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record AchievementRef
{
    /// <summary>The achievement ID.</summary>
    public required int Id { get; init; }

    /// <summary>Describes expansions requirements to access this achievement.</summary>
    public required ProductRequirement? RequiredAccess { get; init; }

    /// <summary>Contains various modifiers that affect how achievements behave.</summary>
    public required AchievementFlags Flags { get; init; }

    /// <summary>The minimum and/or maximum level required to access this achievement.</summary>
    public required LevelRequirement? Level { get; init; }
}
