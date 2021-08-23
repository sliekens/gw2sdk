using GW2SDK.Commerce.Exchange;
using GW2SDK.Commerce.Listings;
using GW2SDK.Commerce.Prices;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Commerce
{
    [PublicAPI]
    public sealed class TradingPostReader : ITradingPostReader
    {
        public IJsonReader<int> Id { get; } = new Int32JsonReader();

        public IJsonReader<ItemPrice> ItemPrice { get; } = new ItemPriceReader();

        public IJsonReader<OrderBook> OrderBook { get; } = new OrderBookReader();

        public IJsonReader<GemsForGoldExchange> GemsForGold { get; } = new GemsForGoldReader();

        public IJsonReader<GoldForGemsExchange> GoldForGems { get; } = new GoldForGemsReader();
    }
}
