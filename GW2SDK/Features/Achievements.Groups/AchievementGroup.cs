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
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Description { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Order { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public int[] Categories { get; set; }
    }
}
