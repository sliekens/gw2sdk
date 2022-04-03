using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Transactions
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record Transaction
    {
        public long Id { get; init; }

        public int ItemId { get; init; }

        public Coin Price { get; init; }

        public int Quantity { get; init; }

        public DateTimeOffset Created { get; init; }

        public DateTimeOffset Executed { get; init; }
    }
}
