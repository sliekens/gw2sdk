using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Exchange
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record GemsForGoldExchange
    {
        public Coin CoinsToReceive { get; init; }

        public Coin CoinsPerGem { get; init; }
    }
}
