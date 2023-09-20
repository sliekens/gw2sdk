namespace GuildWars2.Items;

[PublicAPI]
[Inheritable]
public record Weapon : Item
{
    public required int DefaultSkin { get; init; }

    public required DamageType DamageType { get; init; }

    public required int MinPower { get; init; }

    public required int MaxPower { get; init; }

    public required int Defense { get; init; }

    public required IReadOnlyCollection<InfusionSlot> InfusionSlots { get; init; }

    public required double AttributeAdjustment { get; init; }

    public required InfixUpgrade? Prefix { get; init; }

    public required int? SuffixItemId { get; init; }

    public required int? SecondarySuffixItemId { get; init; }

    public required IReadOnlyCollection<int>? StatChoices { get; init; }
}
