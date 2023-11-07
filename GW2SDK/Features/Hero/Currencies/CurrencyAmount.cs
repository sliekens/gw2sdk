namespace GuildWars2.Hero.Currencies;

[PublicAPI]
[DataTransferObject]
public sealed record CurrencyAmount
{
    public required int CurrencyId { get; init; }

    public required int Amount { get; init; }
}
