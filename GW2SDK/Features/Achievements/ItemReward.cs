using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed class ItemReward : AchievementReward
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Count { get; set; }
    }
}
