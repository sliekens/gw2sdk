namespace GuildWars2.Commerce.Exchange;

/// <summary>Information about the current exchange rate of gold to gems.</summary>
[DataTransferObject]
public sealed record GoldToGems
{
    /// <summary>The amount of gems received for the specified amount of gold.</summary>
    public required int Gems { get; init; }

    /// <summary>The exchange rate represented as the amount of gold per gem.</summary>
    public required Coin ExchangeRate { get; init; }
}
