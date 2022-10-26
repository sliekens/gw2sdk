using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Guilds.Upgrades;

[PublicAPI]
[DataTransferObject]
public sealed record GuildUpgradeCost
{
    public GuildUpgradeCostKind Kind { get; init; }

    public string Name { get; init; } = "";

    public int Count { get; init; }

    public int? ItemId { get; init; }
}