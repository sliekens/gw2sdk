using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
[DataTransferObject]
public sealed record ItemUpgrade
{
    public required UpgradeType Upgrade { get; init; }

    public required int ItemId { get; init; }
}
