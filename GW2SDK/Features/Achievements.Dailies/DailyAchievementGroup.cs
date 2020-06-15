using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    [DataTransferObject]
    public sealed class DailyAchievementGroup
    {
        [JsonProperty(Required = Required.Always)]
        public DailyAchievement[] Pve { get; set; } = new DailyAchievement[0];

        [JsonProperty(Required = Required.Always)]
        public DailyAchievement[] Pvp { get; set; } = new DailyAchievement[0];

        [JsonProperty(Required = Required.Always)]
        public DailyAchievement[] Wvw { get; set; } = new DailyAchievement[0];

        [JsonProperty(Required = Required.Always)]
        public DailyAchievement[] Fractals { get; set; } = new DailyAchievement[0];

        [JsonProperty(Required = Required.Always)]
        public DailyAchievement[] Special { get; set; } = new DailyAchievement[0];
    }
}
