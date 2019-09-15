using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class UpgradeAttribute
    {
        [JsonProperty(Required = Required.Always)]
        public UpgradeAttributeName Attribute { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Modifier { get; set; }
    }
}
