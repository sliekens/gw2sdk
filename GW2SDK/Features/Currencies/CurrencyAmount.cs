using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Currencies;

[PublicAPI]
[DataTransferObject]
public sealed record CurrencyAmount
{
    public required int CurrencyId { get; init; }

    public required int Amount { get; init; }
}
