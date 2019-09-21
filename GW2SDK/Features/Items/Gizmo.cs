using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    public class Gizmo : Item
    {
        [JsonProperty(Required = Required.Always)]
        public int Level { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public int[] VendorIds { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public int? GuildUpgradeId { get; set; }
    }
}
