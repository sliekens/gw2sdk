using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Exchange;

[PublicAPI]
[DataTransferObject]
public sealed record GoldForGemsExchange
{
    public required int GemsToReceive { get; init; }

    public required Coin CoinsPerGem { get; init; }
}
