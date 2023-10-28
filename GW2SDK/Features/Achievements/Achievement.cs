namespace GuildWars2.Achievements;

/// <summary>The base type for information about an achievement. Cast it to a subtype </summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Achievement
{
    /// <summary>The achievement ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the achievement as it appears in the achievement panel.</summary>
    public required string Name { get; init; }

    /// <summary>The URI of the icon as it appears in the achievement panel.</summary>
    /// <remarks>Can be empty. Unfortunately many icons are missing.</remarks>
    public required string Icon { get; init; }

    /// <summary>An informational description like where to start the achievement or a piece of trivia, as it appears in the
    /// achievement tooltip or details.</summary>
    /// <remarks>Can be empty.</remarks>
    public required string Description { get; init; }

    /// <summary>Describes what you need to do to complete the achievement.</summary>
    /// <remarks>Can be empty.</remarks>
    public required string Requirement { get; init; }

    /// <summary>Describes what you need to do to unlock the achievement like which unlock item you need to use or which
    /// achievement you need to complete first.</summary>
    /// <remarks>Can be empty if the achievement is always unlocked. Check whether the <see cref="Flags" /> contains the
    /// <see cref="AchievementFlags.RequiresUnlock" /> flag.</remarks>
    public required string LockedText { get; init; }

    /// <summary>Contains various modifiers that affect how achievements behave.</summary>
    public required AchievementFlags Flags { get; init; }

    public required IReadOnlyCollection<AchievementTier> Tiers { get; init; }

    public required IReadOnlyCollection<AchievementReward>? Rewards { get; init; }

    public required IReadOnlyCollection<AchievementBit>? Bits { get; init; }

    public required IReadOnlyCollection<int>? Prerequisites { get; init; }

    /// <remarks>Can be -1 for repeatable achievements that don't award points.</remarks>
    public required int? PointCap { get; init; }
}
