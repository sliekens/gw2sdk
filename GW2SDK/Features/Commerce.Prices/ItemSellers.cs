using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Commerce.Prices
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class ItemSellers
    {
        [JsonProperty("quantity", Required = Required.Always)]
        public int OpenSellOrders { get; set; }

        [JsonProperty("unit_price", Required = Required.Always)]
        public int BestAsk { get; set; }
    }
}