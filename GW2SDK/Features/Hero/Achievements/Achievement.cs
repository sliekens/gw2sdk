using GuildWars2.Hero.Achievements.Bits;
using GuildWars2.Hero.Achievements.Rewards;

namespace GuildWars2.Hero.Achievements;

/// <summary>Information about an achievement.</summary>
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
    public required string IconHref { get; init; }

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

    /// <summary>Describes the tiers of the achievement. Each tier has a number of things you need to do to complete it and a
    /// number of points awarded.</summary>
    public required IReadOnlyList<AchievementTier> Tiers { get; init; }

    /// <summary>Describes the rewards for completing this achievement. The list type is abstact, the derived types are
    /// <see cref="CoinsReward" />, <see cref="ItemReward" />, <see cref="MasteryPointReward" /> or <see cref="TitleReward" />.</summary>
    public required IReadOnlyList<AchievementReward>? Rewards { get; init; }

    /// <summary>Describes the individual bits of progress that can be made. The list type is abstract. If the current
    /// achievement is a <see cref="CollectionAchievement" /> then the list items are of type <see cref="AchievementItemBit" />
    /// , <see cref="AchievementSkinBit" /> or <see cref="AchievementMinipetBit" />. For regular achievements, the type is
    /// <see cref="AchievementTextBit" /> which is just a description of what is needed.</summary>
    public required IReadOnlyList<AchievementBit>? Bits { get; init; }

    /// <summary>A list of achievement IDs which need to be completed before this achievement can be progressed.</summary>
    /// <remarks>Typically contains a single ID or no IDs at all, but this might change.</remarks>
    public required IReadOnlyList<int> Prerequisites { get; init; }

    /// <summary>The maximum number of achievement points that can be obtained if this is a repeatable achievement. Check
    /// whether the <see cref="Flags" /> contain the <see cref="AchievementFlags.Repeatable" /> flag.</summary>
    /// <remarks> -1 for repeatable achievements that don't award points.</remarks>
    public required int? PointCap { get; init; }
}
