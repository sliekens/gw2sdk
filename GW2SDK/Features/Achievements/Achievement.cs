using System.Diagnostics;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Achievements;
using Newtonsoft.Json;

namespace GW2SDK.Features.Achievements
{
    [DebuggerDisplay("{Name,nq}")]
    [Inheritable]
    [DataTransferObject]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(AchievementDiscriminatorOptions))]
    public class Achievement
    {
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        [CanBeNull]
        public string Icon { get; set; }

        /// <remarks>Description can be an empty string but not null.</remarks>
        [NotNull]
        public string Description { get; set; }

        /// <remarks>Requirement can be an empty string but not null.</remarks>
        [NotNull]
        public string Requirement { get; set; }

        /// <remarks>LockedText can be an empty string but not null.</remarks>
        [NotNull]
        public string LockedText { get; set; }

        [NotNull]
        public AchievementFlag[] Flags { get; set; }

        [NotNull]
        [ItemNotNull]
        public AchievementTier[] Tiers { get; set; }

        /// <remarks>Rewards can be null but it cannot be an empty array or contain null.</remarks>
        [CanBeNull]
        [ItemNotNull]
        public AchievementReward[] Rewards { get; set; }

        [CanBeNull]
        public AchievementBit[] Bits { get; set; }

        [CanBeNull]
        public int[] Prerequisites { get; set; }

        /// <remarks>Can be -1 for repeatable achievements that don't award points.</remarks>
        public int? PointCap { get; set; }
    }
}
