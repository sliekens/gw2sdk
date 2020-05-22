using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed class AchievementTextBit : AchievementBit
    {
        [JsonProperty(Required = Required.Always)]
        public string Text { get; set; } = "";
    }
}
