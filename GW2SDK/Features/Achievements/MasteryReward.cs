using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed class MasteryReward : AchievementReward
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public MasteryRegionName Region { get; set; }
    }
}
