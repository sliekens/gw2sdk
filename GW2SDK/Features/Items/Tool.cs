using GW2SDK.Annotations;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Items.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(ToolDiscriminatorOptions))]
    public class Tool : Item
    {
        public int Level { get; set; }
    }
}
