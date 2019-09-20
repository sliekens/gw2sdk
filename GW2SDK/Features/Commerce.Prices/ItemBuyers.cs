using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Commerce.Prices
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class ItemBuyers
    {
        [JsonProperty("quantity", Required = Required.Always)]
        public int OpenBuyOrders { get; set; }

        [JsonProperty("unit_price", Required = Required.Always)]
        public int BestBid { get; set; }
    }
}