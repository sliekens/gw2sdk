using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Exchange
{
    [PublicAPI]
    public interface IExchangeReader
    {
        IJsonReader<GemsForGoldExchange> GemsForGold { get; }

        IJsonReader<GoldForGemsExchange> GoldForGems { get; }
    }
}