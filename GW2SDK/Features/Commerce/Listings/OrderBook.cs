using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Listings
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record OrderBook
    {
        public int Id { get; init; }

        public IReadOnlyCollection<OrderBookLine> Demand { get; init; } = Array.Empty<OrderBookLine>();

        public IReadOnlyCollection<OrderBookLine> Supply { get; init; } = Array.Empty<OrderBookLine>();
    }
}
