using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
[DataTransferObject]
public sealed record UpgradeAttribute
{
    public required UpgradeAttributeName Attribute { get; init; }

    public required int Modifier { get; init; }
}
