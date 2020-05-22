using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class BackItem : Equipment
    {
        [JsonProperty(Required = Required.Always)]
        public string DefaultSkin { get; set; } = "";
    }
}
