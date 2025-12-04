namespace GuildWars2.Hero.Wallet;

/// <summary>Information about the amount of a currency in the account wallet.</summary>
[DataTransferObject]
public sealed record CurrencyAmount
{
    /// <summary>The currency ID.</summary>
    public required int CurrencyId { get; init; }

    /// <summary>The amount of the currency in the account wallet.</summary>
    public required int Amount { get; init; }
}
