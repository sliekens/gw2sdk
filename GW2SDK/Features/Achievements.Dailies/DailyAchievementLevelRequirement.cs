using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public sealed class DailyAchievementLevelRequirement
    {
        [JsonProperty(Required = Required.Always)]
        public int Min { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Max { get; set; }

        private string GetDebuggerDisplay() => Min == Max ? Min.ToString() : $"{Min}–{Max}";
    }
}
