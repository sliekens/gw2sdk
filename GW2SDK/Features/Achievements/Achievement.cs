using System.Diagnostics;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [Inheritable]
    [DataTransferObject]
    public class Achievement
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Icon { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Description { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public string Requirement { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public string LockedText { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public AchievementFlag[] Flags { get; set; } = new AchievementFlag[0];

        [JsonProperty(Required = Required.Always)]
        public AchievementTier[] Tiers { get; set; } = new AchievementTier[0];

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public AchievementReward[]? Rewards { get; set; }

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public AchievementBit[]? Bits { get; set; }

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int[]? Prerequisites { get; set; }

        /// <remarks>Can be -1 for repeatable achievements that don't award points.</remarks>
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? PointCap { get; set; }
    }
}
