using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
[DataTransferObject]
public sealed record UpgradeAttribute
{
    public required UpgradeAttributeName Attribute { get; init; }

    public required int Modifier { get; init; }
}
