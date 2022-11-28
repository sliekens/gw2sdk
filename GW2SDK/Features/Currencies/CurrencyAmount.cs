using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Currencies;

[PublicAPI]
[DataTransferObject]
public sealed record CurrencyAmount
{
    public required int CurrencyId { get; init; }

    public required int Amount { get; init; }
}
