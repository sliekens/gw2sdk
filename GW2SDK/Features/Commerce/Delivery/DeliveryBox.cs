using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Delivery;

[PublicAPI]
[DataTransferObject]
public sealed record DeliveryBox
{
    public required Coin Coins { get; init; }

    public required IReadOnlyCollection<DeliveredItem> Items { get; init; }
}
