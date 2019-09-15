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
        public string Icon { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public int[] Achievements { get; set; }
    }
}
