using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Commerce.Exchange;

[PublicAPI]
[DataTransferObject]
public sealed record GoldForGemsExchange
{
    public required int GemsToReceive { get; init; }

    public required Coin CoinsPerGem { get; init; }
}
