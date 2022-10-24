using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
[Inheritable]
public record Trinket : Item
{
    public IReadOnlyCollection<InfusionSlot> InfusionSlots { get; init; } =
        Array.Empty<InfusionSlot>();

    public double AttributeAdjustment { get; init; }

    public InfixUpgrade? Prefix { get; init; }

    public int? SuffixItemId { get; init; }

    public IReadOnlyCollection<int>? StatChoices { get; init; }

    public IReadOnlyCollection<ItemUpgrade>? UpgradesInto { get; init; }

    public IReadOnlyCollection<ItemUpgrade>? UpgradesFrom { get; init; }
}
