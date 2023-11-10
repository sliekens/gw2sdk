namespace GuildWars2.Hero.Wallet;

[PublicAPI]
[DataTransferObject]
public sealed record CurrencyAmount
{
    public required int CurrencyId { get; init; }

    public required int Amount { get; init; }
}
