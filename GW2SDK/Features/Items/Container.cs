using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Items;
using Newtonsoft.Json;

namespace GW2SDK.Features.Items
{
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(ContainerDiscriminatorOptions))]
    public class Container : Item
    {
        public int Level { get; set; }
    }
}
