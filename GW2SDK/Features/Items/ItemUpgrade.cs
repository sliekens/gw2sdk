using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
[DataTransferObject]
public sealed record ItemUpgrade
{
    public UpgradeType Upgrade { get; init; }

    public int ItemId { get; init; }
}
