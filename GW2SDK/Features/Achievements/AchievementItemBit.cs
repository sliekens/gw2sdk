using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed class AchievementItemBit : AchievementBit
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }
    }
}
