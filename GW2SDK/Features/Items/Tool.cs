using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Items;
using Newtonsoft.Json;

namespace GW2SDK.Features.Items
{
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(ToolDiscriminatorOptions))]
    public class Tool : Item
    {
        public int Level { get; set; }
    }
}
