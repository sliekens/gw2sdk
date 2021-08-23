using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Listings
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record OrderBook
    {
        public int Id { get; init; }

        public OrderBookLine[] Demand { get; init; } = new OrderBookLine[0];

        public OrderBookLine[] Supply { get; init; } = new OrderBookLine[0];
    }
}
