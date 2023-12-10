namespace GuildWars2.Items;

[PublicAPI]
[Inheritable]
public record UpgradeComponent : Item
{
    public required UpgradeComponentFlags UpgradeComponentFlags { get; init; }

    public required InfusionSlotFlags InfusionUpgradeFlags { get; init; }

    public required double AttributeAdjustment { get; init; }

    public required InfixUpgrade Suffix { get; init; }

    public required string SuffixName { get; init; }
}
