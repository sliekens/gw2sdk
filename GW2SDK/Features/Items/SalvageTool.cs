using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class SalvageTool : Tool
    {
        [JsonProperty(Required = Required.Always)]
        public int Charges { get; set; }
    }
}
