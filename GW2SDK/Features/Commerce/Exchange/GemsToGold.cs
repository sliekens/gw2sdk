namespace GuildWars2.Commerce.Exchange;

/// <summary>Information about the current exchange rate of gems to gold.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record GemsToGold
{
    /// <summary>The amount of gold received for the specified amount of gems.</summary>
    public required Coin Gold { get; init; }

    /// <summary>The exchange rate represented as the amount of gold per gem.</summary>
    public required Coin ExchangeRate { get; init; }
}
