using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Delivery;

[PublicAPI]
[DataTransferObject]
public sealed record DeliveredItem
{
    public int Id { get; init; }

    public int Count { get; init; }
}
