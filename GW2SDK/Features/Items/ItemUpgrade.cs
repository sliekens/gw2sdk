using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class ItemUpgrade
    {
        [JsonProperty(Required = Required.Always)]
        public UpgradeType Upgrade { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int ItemId { get; set; }
    }
}
