using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Achievements;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Achievement
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    /// <summary>The icon URI.</summary>
    /// <remarks>Can be empty.</remarks>
    public required string Icon { get; init; }

    public required string Description { get; init; }

    public required string Requirement { get; init; }

    public required string LockedText { get; init; }

    public required IReadOnlyCollection<AchievementFlag> Flags { get; init; }

    public required IReadOnlyCollection<AchievementTier> Tiers { get; init; }

    public required IReadOnlyCollection<AchievementReward>? Rewards { get; init; }

    public required IReadOnlyCollection<AchievementBit>? Bits { get; init; }

    public required IReadOnlyCollection<int>? Prerequisites { get; init; }

    /// <remarks>Can be -1 for repeatable achievements that don't award points.</remarks>
    public required int? PointCap { get; init; }
}
