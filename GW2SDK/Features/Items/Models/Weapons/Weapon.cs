﻿using GuildWars2.Hero;

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

    public required IReadOnlyList<InfusionSlot> InfusionSlots { get; init; }

    public required double AttributeAdjustment { get; init; }

    public required int? AttributeCombinationId { get; init; }

    public required IDictionary<AttributeName, int> Attributes { get; init; }

    public required Buff? Buff { get; init; }

    public required int? SuffixItemId { get; init; }

    public required int? SecondarySuffixItemId { get; init; }

    public required IReadOnlyList<int>? StatChoices { get; init; }
}
