using System.Diagnostics;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;
using static GW2SDK.Achievements.Dailies.AccessCondition;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public sealed class DailyAchievementProductRequirement
    {
        [JsonProperty(Required = Required.Always)]
        public ProductName Product { get; set; }

        [JsonProperty(Required = Required.Always)]
        public AccessCondition Condition { get; set; }

        private string GetDebuggerDisplay() => Condition == HasAccess ? Product.ToString() : $"!{Product}";
    }
}
