using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    [Inheritable]
    [DataTransferObject]
    public record Achievement
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        /// <summary>The icon URI.</summary>
        /// <remarks>Can be empty.</remarks>
        public string Icon { get; init; } = "";

        public string Description { get; init; } = "";

        public string Requirement { get; init; } = "";

        public string LockedText { get; init; } = "";

        public AchievementFlag[] Flags { get; init; } = Array.Empty<AchievementFlag>();

        public AchievementTier[] Tiers { get; init; } = Array.Empty<AchievementTier>();

        public AchievementReward[]? Rewards { get; init; }

        public AchievementBit[]? Bits { get; init; }

        public int[]? Prerequisites { get; init; }

        /// <remarks>Can be -1 for repeatable achievements that don't award points.</remarks>
        public int? PointCap { get; init; }
    }
}
