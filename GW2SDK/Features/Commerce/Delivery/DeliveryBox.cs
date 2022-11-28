using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Commerce.Delivery;

[PublicAPI]
[DataTransferObject]
public sealed record DeliveryBox
{
    public required Coin Coins { get; init; }

    public required IReadOnlyCollection<DeliveredItem> Items { get; init; }
}
