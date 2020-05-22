using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Achievements.Categories
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class AchievementCategory
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public string Description { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public int Order { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Icon { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public int[] Achievements { get; set; } = new int[0];
    }
}
