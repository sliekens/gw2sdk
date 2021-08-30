using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Listings
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record OrderBook
    {
        public int Id { get; init; }

        public OrderBookLine[] Demand { get; init; } = Array.Empty<OrderBookLine>();

        public OrderBookLine[] Supply { get; init; } = Array.Empty<OrderBookLine>();
    }
}
