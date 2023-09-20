namespace GuildWars2.Commerce.Exchange;

[PublicAPI]
[DataTransferObject]
public sealed record GemsForGoldExchange
{
    public required Coin CoinsToReceive { get; init; }

    public required Coin CoinsPerGem { get; init; }
}
