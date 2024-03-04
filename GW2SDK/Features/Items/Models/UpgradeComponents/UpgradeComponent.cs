using GuildWars2.Hero;

namespace GuildWars2.Items;

[PublicAPI]
[Inheritable]
public record UpgradeComponent : Item
{
    public required UpgradeComponentFlags UpgradeComponentFlags { get; init; }

    public required InfusionSlotFlags InfusionUpgradeFlags { get; init; }

    public required double AttributeAdjustment { get; init; }

    public required int? AttributeCombinationId { get; init; }

    public required IDictionary<AttributeName, int> Attributes { get; init; }

    public required Buff? Buff { get; init; }

    public required string SuffixName { get; init; }
}
