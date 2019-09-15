using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class AchievementTier
    {
        [JsonProperty(Required = Required.Always)]
        public int Count { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Points { get; set; }
    }
}
