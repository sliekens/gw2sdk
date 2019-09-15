using System.Diagnostics;
using GW2SDK.Achievements.Impl;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [Inheritable]
    [DataTransferObject]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(AchievementDiscriminatorOptions))]
    public class Achievement
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Icon { get; set; }

        /// <remarks>Description can be an empty string but not null.</remarks>
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Description { get; set; }

        /// <remarks>Requirement can be an empty string but not null.</remarks>
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Requirement { get; set; }

        /// <remarks>LockedText can be an empty string but not null.</remarks>
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string LockedText { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public AchievementFlag[] Flags { get; set; }

        [NotNull]
        [ItemNotNull]
        [JsonProperty(Required = Required.Always)]
        public AchievementTier[] Tiers { get; set; }

        /// <remarks>Rewards can be null but it cannot be an empty array or contain null.</remarks>
        [CanBeNull]
        [ItemNotNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public AchievementReward[] Rewards { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public AchievementBit[] Bits { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public int[] Prerequisites { get; set; }

        /// <remarks>Can be -1 for repeatable achievements that don't award points.</remarks>
        [JsonProperty(Required = Required.DisallowNull)]
        public int? PointCap { get; set; }
    }
}
