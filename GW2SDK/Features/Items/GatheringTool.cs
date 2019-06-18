using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Items;
using Newtonsoft.Json;

namespace GW2SDK.Features.Items
{
    [PublicAPI]
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(GatheringToolDiscriminatorOptions))]
    public class GatheringTool : Item
    {
        public int Level { get; set; }
    }
}