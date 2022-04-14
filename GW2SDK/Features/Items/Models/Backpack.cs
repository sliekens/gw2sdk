using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace GW2SDK.Items.Models;

[PublicAPI]
public sealed record Backpack : Item
{
    public int DefaultSkin { get; init; }

    public IReadOnlyCollection<InfusionSlot> InfusionSlots { get; init; } = Array.Empty<InfusionSlot>();

    public double AttributeAdjustment { get; init; }

    public InfixUpgrade? Prefix { get; init; }

    public int? SuffixItemId { get; init; }

    public IReadOnlyCollection<int>? StatChoices { get; init; }

    public IReadOnlyCollection<ItemUpgrade>? UpgradesInto { get; init; }

    public IReadOnlyCollection<ItemUpgrade>? UpgradesFrom { get; init; }
}