using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Exchange;

[PublicAPI]
[DataTransferObject]
public sealed record GemsForGoldExchange
{
    public required Coin CoinsToReceive { get; init; }

    public required Coin CoinsPerGem { get; init; }
}
