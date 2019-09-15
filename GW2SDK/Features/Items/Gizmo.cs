using GW2SDK.Annotations;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Items.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(GizmoDiscriminatorOptions))]
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
