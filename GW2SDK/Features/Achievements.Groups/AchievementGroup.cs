using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Achievements.Groups
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class AchievementGroup
    {
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public string Description { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public int Order { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int[] Categories { get; set; } = new int[0];
    }
}
