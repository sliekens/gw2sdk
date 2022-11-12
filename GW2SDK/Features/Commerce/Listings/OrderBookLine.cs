using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Listings;

[PublicAPI]
[DataTransferObject]
public sealed record OrderBookLine
{
    public required int Listings { get; init; }

    public required int Quantity { get; init; }

    public required Coin UnitPrice { get; init; }
}
