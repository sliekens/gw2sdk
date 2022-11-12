using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Guilds.Upgrades;

[PublicAPI]
[DataTransferObject]
public sealed record GuildUpgradeCost
{
    public required GuildUpgradeCostKind Kind { get; init; }

    public required string Name { get; init; }

    public required int Count { get; init; }

    public required int? ItemId { get; init; }
}
