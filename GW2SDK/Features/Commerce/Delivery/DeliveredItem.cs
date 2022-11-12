using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Delivery;

[PublicAPI]
[DataTransferObject]
public sealed record DeliveredItem
{
    public required int Id { get; init; }

    public required int Count { get; init; }
}
