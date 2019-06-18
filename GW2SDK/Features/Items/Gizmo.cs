using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Items;
using Newtonsoft.Json;

namespace GW2SDK.Features.Items
{
    [PublicAPI]
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(GizmoDiscriminatorOptions))]
    public class Gizmo : Item
    {
        public int Level { get; set; }

        [CanBeNull]
        public int[] VendorIds { get; set; }

        public int? GuildUpgradeId { get; set; }
    }
}