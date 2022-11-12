using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Transactions;

[PublicAPI]
[DataTransferObject]
public sealed record Transaction
{
    public required long Id { get; init; }

    public required int ItemId { get; init; }

    public required Coin Price { get; init; }

    public required int Quantity { get; init; }

    public required DateTimeOffset Created { get; init; }

    public required DateTimeOffset Executed { get; init; }
}
