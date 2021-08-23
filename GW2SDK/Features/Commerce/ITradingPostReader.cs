using GW2SDK.Commerce.Exchange;
using GW2SDK.Commerce.Listings;
using GW2SDK.Commerce.Prices;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Commerce
{
    [PublicAPI]
    public interface ITradingPostReader
    {
        IJsonReader<int> Id { get; }

        IJsonReader<ItemPrice> ItemPrice { get; }

        IJsonReader<OrderBook> OrderBook { get; }
        
        IJsonReader<GemsForGoldExchange> GemsForGold { get; }

        IJsonReader<GoldForGemsExchange> GoldForGems { get; }
    }
}
