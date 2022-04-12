using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Currencies.Models;

[PublicAPI]
[DataTransferObject]
public sealed record CurrencyAmount
{
    public int CurrencyId { get; init; }

    public int Amount { get; init; }
}