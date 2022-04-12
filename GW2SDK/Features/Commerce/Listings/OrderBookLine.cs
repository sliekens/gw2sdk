using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Commerce.Listings;

[PublicAPI]
[DataTransferObject]
public sealed record OrderBookLine
{
    public int Listings { get; init; }

    public int Quantity { get; init; }

    public Coin UnitPrice { get; init; }
}