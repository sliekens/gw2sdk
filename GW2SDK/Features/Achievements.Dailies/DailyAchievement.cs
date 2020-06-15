using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    [DebuggerDisplay("{Id}")]
    public sealed class DailyAchievement
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public DailyAchievementLevelRequirement Level { get; set; } = new DailyAchievementLevelRequirement();

        [JsonProperty(Required = Required.DisallowNull)]
        public DailyAchievementProductRequirement? RequiredAccess { get; set; }
    }
}
