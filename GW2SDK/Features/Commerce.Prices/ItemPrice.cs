using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Commerce.Prices
{
    [PublicAPI]
    [DataTransferObject]
    public sealed class ItemPrice
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public bool Whitelisted { get; set; }

        [JsonProperty("buys", Required = Required.Always)]
        public ItemBuyers Buyers { get; set; }

        [JsonProperty("sells", Required = Required.Always)]
        public ItemSellers Sellers { get; set; }
    }
}
